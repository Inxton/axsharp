#!/usr/bin/env pwsh
<#
.SYNOPSIS
Scans the whole repository for vulnerable npm and NuGet dependencies and applies safe fixes.

.DESCRIPTION
One command to remediate moderate-and-above security advisories across both ecosystems:

  NuGet  - iterates every .csproj, restores it, runs `dotnet list package --vulnerable
           --include-transitive`, dedupes advisories, SKIPS AXSharp.*/Inxton.Operon.* (those are
           owned by scripts/update_axsharp_versions.ps1), resolves the minimum safe version from
           the GitHub Advisory Database, and writes the bump to Directory.Packages.props
           (direct = bump in place, transitive = add a new pinned <PackageVersion>).

  npm    - audits the 6 source package.json projects (installing node_modules when missing) and
           runs `npm audit fix` (safe, no --force). Advisories that would need a breaking major
           bump are reported as unfixed.

A timestamped Markdown + JSON report is written to scripts/reports/. With -CreatePR the changes
are committed to branch 'fix/vulnerable-dependencies' off dev and a PR is opened against dev.

Exit code is non-zero when vulnerabilities at/above -MinSeverity remain unfixed, so CI can gate on
it.

.PARAMETER DryRun
Preview everything; make no file changes, no commits, no PR. (Still exits non-zero if vulns found.)

.PARAMETER NpmOnly
Scan/fix npm only.

.PARAMETER NuGetOnly
Scan/fix NuGet only.

.PARAMETER CreatePR
Commit fixes to branch 'fix/vulnerable-dependencies' off origin/dev and open a PR against dev.
Cannot be combined with -DryRun.

.PARAMETER MinSeverity
Lowest severity to act on: low | moderate | high | critical. Default: moderate.

.PARAMETER Source
NuGet v3 feed used to look up available versions (public packages). Default nuget.org.

.PARAMETER Token
Token for the GitHub Advisory API (avoids rate limits). Falls back to NUGET_TOKEN /
GITHUB_PACKAGES_TOKEN / GITHUB_TOKEN / GH_TOKEN.

.PARAMETER Detailed
Verbose logging.

.EXAMPLE
./update-vulnerable-deps.ps1 -DryRun -Detailed

.EXAMPLE
./update-vulnerable-deps.ps1 -NuGetOnly

.EXAMPLE
./update-vulnerable-deps.ps1 -CreatePR
#>

[CmdletBinding()]
param(
    [switch]$DryRun,
    [switch]$NpmOnly,
    [switch]$NuGetOnly,
    [switch]$CreatePR,
    [ValidateSet('low','moderate','high','critical')]
    [string]$MinSeverity = 'low',
    [string]$Source = 'https://api.nuget.org/v3/index.json',
    [string]$Token,
    [switch]$Detailed
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
. "$scriptRoot/_deps-common.ps1"

$repoRoot = Split-Path -Parent $scriptRoot
$propsPath = Join-Path $repoRoot 'Directory.Packages.props'

# ---------------------------------------------------------------------------
# Validation & constants
# ---------------------------------------------------------------------------
if($DryRun -and $CreatePR){ Write-Err '-DryRun and -CreatePR cannot be combined.'; exit 1 }
if($NpmOnly -and $NuGetOnly){ Write-Err '-NpmOnly and -NuGetOnly cannot be combined.'; exit 1 }

$doNuget = -not $NpmOnly
$doNpm   = -not $NuGetOnly

$SeverityRank = @{ low = 1; moderate = 2; high = 3; critical = 4 }
$minRank = $SeverityRank[$MinSeverity]

# Packages owned by update_axsharp_versions.ps1 - never touched here.
$SkipPattern = '^(AXSharp|Inxton\.Operon|AXOpen)\b'

$Token = Resolve-FeedToken -Token $Token -Detailed:$Detailed

# Source npm projects - discovered dynamically under src/ (skips node_modules and
# bin/obj/ctrl generated copies so only first-party source manifests are audited).
$NpmProjects = @(
    Get-ChildItem -Path (Join-Path $repoRoot 'src') -Recurse -File -Filter 'package.json' -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -notmatch '[\\/](node_modules|bin|obj|ctrl)[\\/]' } |
        ForEach-Object { $_.FullName } |
        Sort-Object
)

# Accumulators for the report.
$NuGetFixed   = New-Object System.Collections.ArrayList
$NuGetSkipped = New-Object System.Collections.ArrayList
$NuGetUnfixed = New-Object System.Collections.ArrayList
$NpmResults   = New-Object System.Collections.ArrayList
$ScanErrors   = New-Object System.Collections.ArrayList

# ---------------------------------------------------------------------------
# Helpers
# ---------------------------------------------------------------------------
function Test-CommandAvailable {
    param([string]$Name)
    $null -ne (Get-Command $Name -ErrorAction SilentlyContinue)
}

function Get-GhsaIdFromUrl {
    param([string]$Url)
    if($Url -and ($Url -match '(GHSA-[0-9a-z]{4}-[0-9a-z]{4}-[0-9a-z]{4})')){ return $Matches[1] }
    return $null
}

$script:AdvisoryCache = @{}
function Get-AdvisoryPatchedVersion {
    # Queries the GitHub Advisory Database for the first patched version of $PackageId.
    # Returns the patched version string, or $null when it cannot be determined.
    param([string]$GhsaId,[string]$PackageId)
    if(-not $GhsaId){ return $null }
    if(-not $script:AdvisoryCache.ContainsKey($GhsaId)){
        $headers = @{ 'User-Agent' = 'axopen-update-vulnerable-deps'; 'Accept' = 'application/vnd.github+json' }
        if($Token){ $headers['Authorization'] = "Bearer $Token" }
        try {
            $script:AdvisoryCache[$GhsaId] = Invoke-RestMethod -Uri "https://api.github.com/advisories/$GhsaId" -Headers $headers -TimeoutSec 30
        } catch {
            if($Detailed){ Write-Warn "Advisory lookup failed for ${GhsaId}: $($_.Exception.Message)" }
            $script:AdvisoryCache[$GhsaId] = $null
        }
    }
    $adv = $script:AdvisoryCache[$GhsaId]
    if(-not $adv -or -not $adv.vulnerabilities){ return $null }
    foreach($v in $adv.vulnerabilities){
        if($v.package -and $v.package.ecosystem -eq 'nuget' -and $v.package.name -and ($v.package.name.ToLower() -eq $PackageId.ToLower())){
            $fpv = $v.first_patched_version
            if($null -eq $fpv){ return $null }
            if($fpv -is [string]){ return $fpv }
            if($fpv.PSObject.Properties.Name -contains 'identifier'){ return $fpv.identifier }
        }
    }
    return $null
}

function Resolve-SafeNuGetVersion {
    # Determines the minimum safe version for a vulnerable package:
    #   1. the highest 'first patched version' across the package's advisories, then
    #   2. snapped up to the lowest version actually available on the feed that is >= that patch
    #      and strictly greater than the current resolved version.
    # Returns $null (=> report as unfixed) when no safe version can be determined.
    param([string]$PackageId,[string]$CurrentVersion,[string[]]$GhsaIds,$FeedCtx)
    $patched = $null
    foreach($g in $GhsaIds){
        $p = Get-AdvisoryPatchedVersion -GhsaId $g -PackageId $PackageId
        if($p -and (Test-VersionGreater $p $patched)){ $patched = $p }
    }
    if(-not $patched){ return $null }

    $available = @()
    try { $available = Get-PackageVersionsFromFeed -PkgBase $FeedCtx.PkgBase -Headers $FeedCtx.Headers -PackageId $PackageId } catch { $available = @() }
    if(-not $available -or $available.Count -eq 0){
        # Feed gave us nothing; trust the advisory's patched version if it improves on current.
        if(Test-VersionGreater $patched $CurrentVersion){ return $patched } else { return $null }
    }

    $currentIsPre = Test-IsPrerelease $CurrentVersion
    $candidates = $available |
        Where-Object { ($currentIsPre) -or (-not (Test-IsPrerelease $_)) } |   # stable only unless current is pre
        Where-Object { (Test-VersionGreater $_ $CurrentVersion) -and -not (Test-VersionGreater $patched $_) }  # > current AND >= patched

    if(-not $candidates -or @($candidates).Count -eq 0){ return $null }

    # lowest such candidate = minimum safe bump
    $best = @($candidates)[0]
    foreach($c in $candidates){ if(Test-VersionGreater $best $c){ $best = $c } }
    return $best
}

# ---------------------------------------------------------------------------
# Directory.Packages.props editing
# ---------------------------------------------------------------------------
function Update-PropsDirect {
    # Bumps an existing <PackageVersion Include="ID" Version="..." /> in place.
    # Returns updated content, or $null if the package id was not found.
    param([string]$Content,[string]$PackageId,[string]$NewVersion)
    $idEsc = [regex]::Escape($PackageId)
    # Include before Version (repo convention)
    $rx1 = "(<PackageVersion\b[^>]*\bInclude=`"$idEsc`"[^>]*\bVersion=`")[^`"]*(`")"
    if([regex]::IsMatch($Content,$rx1)){
        return [regex]::Replace($Content,$rx1,"`${1}$NewVersion`${2}",1)
    }
    # Version before Include (defensive)
    $rx2 = "(<PackageVersion\b[^>]*\bVersion=`")[^`"]*(`"[^>]*\bInclude=`"$idEsc`")"
    if([regex]::IsMatch($Content,$rx2)){
        return [regex]::Replace($Content,$rx2,"`${1}$NewVersion`${2}",1)
    }
    return $null
}

function Test-PropsHasPackage {
    param([string]$Content,[string]$PackageId)
    $idEsc = [regex]::Escape($PackageId)
    return [regex]::IsMatch($Content,"<PackageVersion\b[^>]*\bInclude=`"$idEsc`"")
}

function Add-PropsTransitivePin {
    # Adds a <PackageVersion> into a managed "security pins" ItemGroup, creating it before
    # </Project> if absent. Returns updated content.
    param([string]$Content,[string]$PackageId,[string]$NewVersion)
    $marker = '<!-- Security: transitive vulnerability pins (managed by scripts/update-vulnerable-deps.ps1) -->'
    $entry  = "    <PackageVersion Include=`"$PackageId`" Version=`"$NewVersion`" />"
    if($Content.Contains($marker)){
        # Insert as the first entry inside the existing managed ItemGroup.
        $rx = [regex]::Escape($marker) + "(\r?\n\s*<ItemGroup>)"
        return [regex]::Replace($Content,$rx,"`$0`r`n$entry",1)
    }
    $block = "  $marker`r`n  <ItemGroup>`r`n$entry`r`n  </ItemGroup>`r`n</Project>"
    return ($Content -replace '</Project>\s*$',$block)
}

# ---------------------------------------------------------------------------
# NuGet scan & fix
# ---------------------------------------------------------------------------
function Invoke-NuGetScan {
    Write-Info 'Scanning NuGet dependencies...'
    if(-not (Test-CommandAvailable 'dotnet')){ Write-Err 'dotnet CLI not found on PATH.'; [void]$ScanErrors.Add('dotnet CLI not found'); return }

    $csprojFiles = Get-ChildItem -Path (Join-Path $repoRoot 'src') -Recurse -Filter '*.csproj' -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -notmatch '[\\/](bin|obj)[\\/]' }
    # Include the cake build project (ships in the repo).
    $cakeProj = Get-ChildItem -Path (Join-Path $repoRoot 'cake') -Recurse -Filter '*.csproj' -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -notmatch '[\\/](bin|obj)[\\/]' }
    $csprojFiles = @($csprojFiles) + @($cakeProj)

    Write-Info "Found $($csprojFiles.Count) project(s) to scan."

    # packageId -> aggregated advisory record
    $vuln = @{}
    $total = $csprojFiles.Count
    $i = 0
    foreach($proj in $csprojFiles){
        $i++
        $pct = if($total -gt 0){ [int](($i-1) / $total * 100) } else { 0 }
        Write-Progress -Activity 'Scanning NuGet projects' `
            -Status ("[$i/$total] $($proj.Name)  -  $($vuln.Count) vulnerable package(s) so far") `
            -CurrentOperation 'restoring...' -PercentComplete $pct
        if($Detailed){ Write-Info "[$i/$total] $($proj.FullName)" }
        try {
            & dotnet restore $proj.FullName --nologo *> $null
        } catch {
            [void]$ScanErrors.Add("restore failed: $($proj.FullName)")
            if($Detailed){ Write-Warn "restore failed: $($proj.Name)" }
            continue
        }
        Write-Progress -Activity 'Scanning NuGet projects' `
            -Status ("[$i/$total] $($proj.Name)  -  $($vuln.Count) vulnerable package(s) so far") `
            -CurrentOperation 'listing vulnerabilities...' -PercentComplete $pct
        $raw = & dotnet list $proj.FullName package --vulnerable --include-transitive --format json 2>$null | Out-String
        if(-not $raw.Trim()){ continue }
        try { $parsed = $raw | ConvertFrom-Json } catch { [void]$ScanErrors.Add("unparseable output: $($proj.FullName)"); continue }
        if(-not ($parsed.PSObject.Properties.Name -contains 'projects')){ continue }
        foreach($p in $parsed.projects){
            if(-not ($p.PSObject.Properties.Name -contains 'frameworks') -or -not $p.frameworks){ continue }
            foreach($fw in $p.frameworks){
                foreach($kind in @('topLevelPackages','transitivePackages')){
                    if(-not ($fw.PSObject.Properties.Name -contains $kind) -or -not $fw.$kind){ continue }
                    $isDirect = ($kind -eq 'topLevelPackages')
                    foreach($pkg in $fw.$kind){
                        if(-not ($pkg.PSObject.Properties.Name -contains 'vulnerabilities') -or -not $pkg.vulnerabilities){ continue }
                        $id = $pkg.id
                        $resolved = $pkg.resolvedVersion
                        foreach($adv in $pkg.vulnerabilities){
                            $sev = ("$($adv.severity)").ToLower()
                            $rank = if($SeverityRank.ContainsKey($sev)){ $SeverityRank[$sev] } else { 0 }
                            if($rank -lt $minRank){ continue }
                            $url = if($adv.PSObject.Properties.Name -contains 'advisoryurl'){ $adv.advisoryurl } else { '' }
                            if(-not $vuln.ContainsKey($id)){
                                $vuln[$id] = [PSCustomObject]@{
                                    Id=$id; Resolved=$resolved; IsDirect=$isDirect; MaxRank=$rank;
                                    Severity=$sev; Urls=(New-Object System.Collections.ArrayList); Projects=(New-Object System.Collections.ArrayList)
                                }
                            }
                            $rec = $vuln[$id]
                            if($isDirect){ $rec.IsDirect = $true }
                            if($rank -gt $rec.MaxRank){ $rec.MaxRank = $rank; $rec.Severity = $sev }
                            if($url -and -not $rec.Urls.Contains($url)){ [void]$rec.Urls.Add($url) }
                            if(-not $rec.Projects.Contains($proj.Name)){ [void]$rec.Projects.Add($proj.Name) }
                        }
                    }
                }
            }
        }
    }

    Write-Progress -Activity 'Scanning NuGet projects' -Completed

    if($vuln.Count -eq 0){ Write-Info 'No NuGet vulnerabilities at/above the threshold.'; return }
    Write-Info "Found $($vuln.Count) vulnerable NuGet package(s) at/above '$MinSeverity'."

    # Resolve safe versions & apply.
    $feedCtx = $null
    try { $feedCtx = Get-FeedContext -Feed $Source -Tok $null } catch { Write-Warn "Could not initialise NuGet feed context: $($_.Exception.Message)" }

    $content = Get-Content -LiteralPath $propsPath -Raw
    $changed = $false

    foreach($id in ($vuln.Keys | Sort-Object)){
        $rec = $vuln[$id]
        if($id -match $SkipPattern){
            [void]$NuGetSkipped.Add([PSCustomObject]@{ Id=$id; Current=$rec.Resolved; Severity=$rec.Severity; Reason='skipped-AXSharp (owned by update_axsharp_versions.ps1)' })
            continue
        }
        $ghsaIds = @($rec.Urls | ForEach-Object { Get-GhsaIdFromUrl $_ } | Where-Object { $_ })
        $safe = $null
        if($feedCtx){ $safe = Resolve-SafeNuGetVersion -PackageId $id -CurrentVersion $rec.Resolved -GhsaIds $ghsaIds -FeedCtx $feedCtx }

        if(-not $safe){
            [void]$NuGetUnfixed.Add([PSCustomObject]@{ Id=$id; Current=$rec.Resolved; Severity=$rec.Severity; Direct=$rec.IsDirect; Advisories=($rec.Urls -join ' '); Reason='no safe version determined (manual review)' })
            continue
        }

        $action = $null
        if(Test-PropsHasPackage -Content $content -PackageId $id){
            $updated = Update-PropsDirect -Content $content -PackageId $id -NewVersion $safe
            if($updated){ $content = $updated; $action = 'bumped (direct)' }
        } else {
            $content = Add-PropsTransitivePin -Content $content -PackageId $id -NewVersion $safe
            $action = 'pinned (transitive)'
        }
        if($action){
            $changed = $true
            [void]$NuGetFixed.Add([PSCustomObject]@{ Id=$id; From=$rec.Resolved; To=$safe; Severity=$rec.Severity; Direct=$rec.IsDirect; Action=$action; Advisories=($rec.Urls -join ' ') })
            Write-Info ("  {0}: {1} -> {2}  [{3}]" -f $id,$rec.Resolved,$safe,$action)
        } else {
            [void]$NuGetUnfixed.Add([PSCustomObject]@{ Id=$id; Current=$rec.Resolved; Severity=$rec.Severity; Direct=$rec.IsDirect; Advisories=($rec.Urls -join ' '); Reason='could not edit Directory.Packages.props' })
        }
    }

    if($changed){
        if($DryRun){
            Write-Warn '[DRY RUN] Directory.Packages.props would be updated (not written).'
        } else {
            Write-Utf8NoBom-LF -Path $propsPath -Content $content
            Write-Info 'Directory.Packages.props updated.'
        }
    }
}

# ---------------------------------------------------------------------------
# npm scan & fix
# ---------------------------------------------------------------------------
function Get-NpmCountsAtOrAbove {
    param($AuditObj)
    $total = 0
    if($AuditObj -and $AuditObj.PSObject.Properties.Name -contains 'metadata' -and $AuditObj.metadata.PSObject.Properties.Name -contains 'vulnerabilities'){
        $v = $AuditObj.metadata.vulnerabilities
        foreach($sev in @('moderate','high','critical')){
            if($SeverityRank[$sev] -ge $minRank -and ($v.PSObject.Properties.Name -contains $sev)){ $total += [int]$v.$sev }
        }
        if($minRank -le 1 -and ($v.PSObject.Properties.Name -contains 'low')){ $total += [int]$v.low }
    }
    return $total
}

function Invoke-NpmScan {
    Write-Info 'Scanning npm dependencies...'
    if(-not (Test-CommandAvailable 'npm')){ Write-Err 'npm not found on PATH.'; [void]$ScanErrors.Add('npm not found'); return }

    $npmTotal = $NpmProjects.Count
    $npmIdx = 0
    foreach($pkgJson in $NpmProjects){
        $npmIdx++
        if(-not (Test-Path -LiteralPath $pkgJson)){ if($Detailed){ Write-Warn "missing: $pkgJson" }; continue }
        $dir = Split-Path -Parent $pkgJson
        $name = (Resolve-Path -LiteralPath $dir).Path.Replace((Resolve-Path $repoRoot).Path,'').TrimStart('\','/')
        Write-Progress -Activity 'Scanning npm projects' -Status "[$npmIdx/$npmTotal] $name" `
            -PercentComplete ([int](($npmIdx-1) / $npmTotal * 100))
        Write-Info "  $name"
        Push-Location $dir
        try {
            if(-not (Test-Path 'node_modules')){
                if($DryRun){ Write-Warn "  [DRY RUN] would run: npm install" }
                else { & npm install --no-audit --no-fund *> $null }
            }

            $beforeRaw = & npm audit --json 2>$null | Out-String
            $before = $null; try { $before = $beforeRaw | ConvertFrom-Json } catch {}
            $beforeCount = Get-NpmCountsAtOrAbove $before

            if($beforeCount -le 0){
                [void]$NpmResults.Add([PSCustomObject]@{ Project=$name; Before=0; After=0; Fixed=0; Note='clean' })
                Pop-Location; continue
            }

            if($DryRun){
                Write-Warn "  [DRY RUN] would run: npm audit fix  ($beforeCount vuln >= $MinSeverity)"
                [void]$NpmResults.Add([PSCustomObject]@{ Project=$name; Before=$beforeCount; After=$beforeCount; Fixed=0; Note='dry-run (not fixed)' })
                Pop-Location; continue
            }

            & npm audit fix *> $null
            $afterRaw = & npm audit --json 2>$null | Out-String
            $after = $null; try { $after = $afterRaw | ConvertFrom-Json } catch {}
            $afterCount = Get-NpmCountsAtOrAbove $after
            $note = if($afterCount -gt 0){ "$afterCount remain (need --force / breaking major)" } else { 'all fixed' }
            [void]$NpmResults.Add([PSCustomObject]@{ Project=$name; Before=$beforeCount; After=$afterCount; Fixed=($beforeCount-$afterCount); Note=$note })
            Write-Info ("    before={0} after={1} ({2})" -f $beforeCount,$afterCount,$note)
        } catch {
            [void]$ScanErrors.Add("npm error in ${name}: $($_.Exception.Message)")
            Write-Warn "  error: $($_.Exception.Message)"
        } finally {
            Pop-Location
        }
    }
    Write-Progress -Activity 'Scanning npm projects' -Completed
}

# ---------------------------------------------------------------------------
# Report
# ---------------------------------------------------------------------------
function Write-Report {
    param([string]$Stamp)
    $reportsDir = Join-Path $scriptRoot 'reports'
    if(-not (Test-Path $reportsDir)){ New-Item -ItemType Directory -Path $reportsDir -Force | Out-Null }
    $mdPath = Join-Path $reportsDir "vuln-report-$Stamp.md"
    $jsonPath = Join-Path $reportsDir "vuln-report-$Stamp.json"

    $payload = [PSCustomObject]@{
        timestamp   = $Stamp
        minSeverity = $MinSeverity
        dryRun      = [bool]$DryRun
        ecosystems  = @{ nuget = [bool]$doNuget; npm = [bool]$doNpm }
        nuget       = @{ fixed = $NuGetFixed; skipped = $NuGetSkipped; unfixed = $NuGetUnfixed }
        npm         = $NpmResults
        scanErrors  = $ScanErrors
    }
    Write-Utf8NoBom-LF -Path $jsonPath -Content ($payload | ConvertTo-Json -Depth 8)

    $sb = New-Object System.Text.StringBuilder
    [void]$sb.AppendLine("# Vulnerable dependency report")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("- Generated: $Stamp")
    [void]$sb.AppendLine("- Min severity: **$MinSeverity**")
    [void]$sb.AppendLine("- Dry run: $([bool]$DryRun)")
    [void]$sb.AppendLine("- Ecosystems: NuGet=$doNuget, npm=$doNpm")
    [void]$sb.AppendLine("")
    if($doNuget){
        [void]$sb.AppendLine("## NuGet")
        [void]$sb.AppendLine("")
        [void]$sb.AppendLine("### Fixed ($($NuGetFixed.Count))")
        [void]$sb.AppendLine("| Package | From | To | Severity | Kind | Action |")
        [void]$sb.AppendLine("|---|---|---|---|---|---|")
        foreach($f in $NuGetFixed){ [void]$sb.AppendLine("| $($f.Id) | $($f.From) | $($f.To) | $($f.Severity) | $(if($f.Direct){'direct'}else{'transitive'}) | $($f.Action) |") }
        [void]$sb.AppendLine("")
        [void]$sb.AppendLine("### Unfixed - manual review ($($NuGetUnfixed.Count))")
        [void]$sb.AppendLine("| Package | Current | Severity | Reason | Advisories |")
        [void]$sb.AppendLine("|---|---|---|---|---|")
        foreach($u in $NuGetUnfixed){ [void]$sb.AppendLine("| $($u.Id) | $($u.Current) | $($u.Severity) | $($u.Reason) | $($u.Advisories) |") }
        [void]$sb.AppendLine("")
        [void]$sb.AppendLine("### Skipped ($($NuGetSkipped.Count))")
        foreach($s in $NuGetSkipped){ [void]$sb.AppendLine("- $($s.Id) ($($s.Current), $($s.Severity)) - $($s.Reason)") }
        [void]$sb.AppendLine("")
    }
    if($doNpm){
        [void]$sb.AppendLine("## npm")
        [void]$sb.AppendLine("")
        [void]$sb.AppendLine("| Project | Before | After | Fixed | Note |")
        [void]$sb.AppendLine("|---|---|---|---|---|")
        foreach($n in $NpmResults){ [void]$sb.AppendLine("| $($n.Project) | $($n.Before) | $($n.After) | $($n.Fixed) | $($n.Note) |") }
        [void]$sb.AppendLine("")
    }
    if($ScanErrors.Count -gt 0){
        [void]$sb.AppendLine("## Scan errors")
        foreach($e in $ScanErrors){ [void]$sb.AppendLine("- $e") }
    }
    Write-Utf8NoBom-LF -Path $mdPath -Content $sb.ToString()

    Write-Info "Report: $mdPath"
    return $mdPath
}

# ---------------------------------------------------------------------------
# Commit + PR
# ---------------------------------------------------------------------------
function Invoke-CreatePR {
    param([string]$ReportPath)
    if(-not (Test-CommandAvailable 'git')){ Write-Err 'git not found; cannot create PR.'; return }
    if(-not (Test-CommandAvailable 'gh')){ Write-Err 'gh CLI not found; cannot open PR.'; return }

    $branch = 'fix/vulnerable-dependencies'
    Write-Info "Creating branch '$branch' off origin/dev and opening PR..."

    & git -C $repoRoot fetch origin dev --quiet
    # Carry working-tree fixes onto a fresh branch from origin/dev.
    & git -C $repoRoot stash push -u -m 'vuln-fix-wip' *> $null
    $stashed = ($LASTEXITCODE -eq 0)
    & git -C $repoRoot switch -C $branch origin/dev
    if($LASTEXITCODE -ne 0){ Write-Err "Failed to create branch $branch."; if($stashed){ & git -C $repoRoot stash pop *> $null }; return }
    if($stashed){
        & git -C $repoRoot stash pop
        if($LASTEXITCODE -ne 0){ Write-Err 'Stash pop conflicted; resolve manually. Aborting PR.'; return }
    }

    & git -C $repoRoot add -- 'Directory.Packages.props' '**/package.json' '**/package-lock.json'
    & git -C $repoRoot commit -m @'
fix(deps): remediate moderate+ npm & NuGet vulnerabilities

Automated by scripts/update-vulnerable-deps.ps1.

Co-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>
'@
    if($LASTEXITCODE -ne 0){ Write-Warn 'Nothing to commit (no changes staged). Skipping PR.'; return }

    & git -C $repoRoot push -u origin $branch
    if($LASTEXITCODE -ne 0){ Write-Err 'git push failed.'; return }

    $fixedLines = ($NuGetFixed | ForEach-Object { "- NuGet $($_.Id): $($_.From) -> $($_.To) ($($_.Severity))" }) -join "`n"
    $npmLines   = ($NpmResults | Where-Object { $_.Fixed -gt 0 } | ForEach-Object { "- npm $($_.Project): fixed $($_.Fixed)" }) -join "`n"
    $unfixed    = ($NuGetUnfixed | ForEach-Object { "- $($_.Id) ($($_.Severity)): $($_.Reason)" }) -join "`n"
    $body = @"
Automated remediation of moderate-and-above npm & NuGet vulnerabilities.

## Fixed
$fixedLines
$npmLines

## Needs manual review
$unfixed

See the attached report ($([System.IO.Path]::GetFileName($ReportPath))) for full detail.

🤖 Generated with [Claude Code](https://claude.com/claude-code)
"@
    & gh pr create --base dev --head $branch --title 'fix(deps): remediate moderate+ npm & NuGet vulnerabilities' --body $body
    if($LASTEXITCODE -ne 0){ Write-Err 'gh pr create failed.' } else { Write-Info 'PR opened against dev.' }
}

# ---------------------------------------------------------------------------
# Main
# ---------------------------------------------------------------------------
$stamp = (Get-Date).ToString('yyyy-MM-dd-HHmmss')
Write-Info "update-vulnerable-deps  (minSeverity=$MinSeverity, dryRun=$DryRun, npm=$doNpm, nuget=$doNuget)"

if($doNuget){ Invoke-NuGetScan }
if($doNpm){ Invoke-NpmScan }

$reportPath = Write-Report -Stamp $stamp

# Determine unfixed count for exit code.
$npmRemaining = ($NpmResults | Measure-Object -Property After -Sum).Sum
if($null -eq $npmRemaining){ $npmRemaining = 0 }
$unfixedCount = $NuGetUnfixed.Count + [int]$npmRemaining

Write-Host ''
Write-Host '================ SUMMARY ================' -ForegroundColor Cyan
Write-Host ("  NuGet fixed   : {0}" -f $NuGetFixed.Count)
Write-Host ("  NuGet skipped : {0}" -f $NuGetSkipped.Count)
Write-Host ("  NuGet unfixed : {0}" -f $NuGetUnfixed.Count)
Write-Host ("  npm projects  : {0}" -f $NpmResults.Count)
Write-Host ("  npm remaining : {0}" -f $npmRemaining)
Write-Host ("  scan errors   : {0}" -f $ScanErrors.Count)
Write-Host '========================================' -ForegroundColor Cyan

if($CreatePR -and -not $DryRun){
    if($NuGetFixed.Count -gt 0 -or ($NpmResults | Where-Object { $_.Fixed -gt 0 })){
        Invoke-CreatePR -ReportPath $reportPath
    } else {
        Write-Warn 'No fixes applied; skipping PR creation.'
    }
}

if($ScanErrors.Count -gt 0){ Write-Warn "$($ScanErrors.Count) scan error(s) - see report." }

if($unfixedCount -gt 0){
    Write-Warn "$unfixedCount vulnerability/vulnerabilities remain unfixed."
    exit 1
}
Write-Info 'No outstanding vulnerabilities at/above threshold.'
exit 0
