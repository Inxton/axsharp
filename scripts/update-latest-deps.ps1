#!/usr/bin/env pwsh
<#
.SYNOPSIS
Updates all (non-AXSharp) dependencies to their latest STABLE versions across NuGet, npm and dotnet tools.

.DESCRIPTION
One command to bring the repo up to the latest stable third-party versions. It complements - and never
overlaps with - the two narrower updaters:

  * scripts/update_axsharp_versions.ps1  - owns AXSharp.*/Inxton.Operon.* (always skipped here)
  * scripts/update-vulnerable-deps.ps1   - owns the "Security pins" ItemGroup (always skipped here)

Scope:

  NuGet  - rewrites <PackageVersion> entries (and the GitVersion.MsBuild <GlobalPackageReference>) in
           Directory.Packages.props to the latest STABLE version from nuget.org. AXSharp/Operon/AXOpen
           packages, the security-pin entries, and framework-tied packages (Microsoft.AspNetCore.*,
           Microsoft.EntityFrameworkCore.*, Microsoft.Extensions.*, System.*, Microsoft.NET.ILLink.Tasks,
           Microsoft.VisualStudio.Web.CodeGeneration.Design) are FROZEN to stay aligned with net10.0.

  npm    - for the source package.json projects, rewrites declared ranges (preserving the ^/~ prefix) to
           the latest stable from `npm outdated`, then runs `npm install` to refresh package-lock.json.

  tools  - rewrites .config/dotnet-tools.json tool versions to latest stable (AXSharp.ix* skipped).

GitVersion.MsBuild and gitversion.tool are resolved together and pinned to the SAME latest stable.

"Latest" means latest STABLE: alpha/beta/rc are skipped. Major-version bumps ARE applied but flagged
prominently in the report (use -SkipMajor to exclude them).

Default action is a DRY RUN - pass -Apply to write changes. With -Apply (and unless -SkipBuild) the full
cake build runs as verification; on failure the offending bump(s) are bisected out and only the green
remainder is kept. A timestamped Markdown + JSON report is written to scripts/reports/. With -CreatePR the
changes are committed to branch 'chore/update-latest-deps' off origin/dev and a PR is opened against dev.

.PARAMETER Apply
Write changes. Without it the script previews only (dry run) and touches nothing.

.PARAMETER NuGetOnly
Process NuGet (Directory.Packages.props) only.

.PARAMETER NpmOnly
Process npm only.

.PARAMETER ToolsOnly
Process dotnet tools only.

.PARAMETER SkipMajor
Exclude major-version bumps (default: apply them and flag them in the report).

.PARAMETER SkipBuild
Skip the cake build verification step (fast path; relies on manual review).

.PARAMETER RollbackAllOnFailure
On build failure, revert every change instead of bisecting for a green subset.

.PARAMETER MaxBisectBuilds
Cap on the number of full cake builds spent bisecting after an initial failure. Default 6.

.PARAMETER CreatePR
Commit changes to branch 'chore/update-latest-deps' off origin/dev and open a PR against dev. Implies -Apply.

.PARAMETER NpmProjects
Explicit list of package.json paths to process (relative to repo root or absolute). When omitted,
projects are discovered automatically under src/ (skipping bin/obj/ctrl/.apax/node_modules/wwwroot/dist).

.PARAMETER Source
NuGet v3 feed used to look up available versions. Default nuget.org.

.PARAMETER Token
Token for the NuGet feed (private feeds). Falls back to NUGET_TOKEN / GITHUB_PACKAGES_TOKEN /
GITHUB_TOKEN / GH_TOKEN.

.PARAMETER Detailed
Verbose logging.

.EXAMPLE
./update-latest-deps.ps1                       # dry run, all ecosystems

.EXAMPLE
./update-latest-deps.ps1 -NuGetOnly            # dry run, NuGet only

.EXAMPLE
./update-latest-deps.ps1 -Apply -SkipBuild     # write changes, skip the (slow) cake build

.EXAMPLE
./update-latest-deps.ps1 -CreatePR             # apply, build-verify, open PR against dev
#>

[CmdletBinding()]
param(
    [switch]$Apply,
    [switch]$NuGetOnly,
    [switch]$NpmOnly,
    [switch]$ToolsOnly,
    [switch]$SkipMajor,
    [switch]$SkipBuild,
    [switch]$RollbackAllOnFailure,
    [int]$MaxBisectBuilds = 6,
    [switch]$CreatePR,
    [string[]]$NpmProjects,
    [string]$Source = 'https://api.nuget.org/v3/index.json',
    [string]$Token,
    [switch]$Detailed
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
. "$scriptRoot/_deps-common.ps1"

$repoRoot  = Split-Path -Parent $scriptRoot
$propsPath = Join-Path $repoRoot 'Directory.Packages.props'
$toolsPath = Join-Path $repoRoot '.config/dotnet-tools.json'
$cakeProj  = Join-Path $repoRoot 'cake/Build.csproj'

# ---------------------------------------------------------------------------
# Validation & constants
# ---------------------------------------------------------------------------
if($CreatePR){ $Apply = $true }                       # -CreatePR implies writing changes
$DryRun = -not $Apply

$onlyCount = @($NuGetOnly,$NpmOnly,$ToolsOnly | Where-Object { $_ }).Count
if($onlyCount -gt 1){ Write-Err '-NuGetOnly / -NpmOnly / -ToolsOnly are mutually exclusive.'; exit 1 }

$doNuget = (-not $NpmOnly) -and (-not $ToolsOnly)
$doNpm   = (-not $NuGetOnly) -and (-not $ToolsOnly)
$doTools = (-not $NuGetOnly) -and (-not $NpmOnly)

# Feed auth: never send a token to the public nuget.org CDN (it 403s on authed cached responses).
# Resolve a token only for an explicitly-provided private -Source (or explicit -Token).
$IsPublicNuGet = ($Source -match 'api\.nuget\.org')
$feedToken = if($Token){ $Token } elseif(-not $IsPublicNuGet){ Resolve-FeedToken -Token $Token -Detailed:$Detailed } else { $null }

# Owned by update_axsharp_versions.ps1 - never touched.
$AxSharpSkipPattern = '^(AXSharp|Inxton\.Operon|AXOpen)\b'
# Owned by update-vulnerable-deps.ps1 ("Security pins" ItemGroup) - never touched.
$SecurityPinIds = @('') #@('Snappier','System.Security.Cryptography.Xml')
# Frozen to stay aligned with net10.0 (see plan / interview).
$FrameworkFreezeExact = @('Microsoft.NET.ILLink.Tasks','Microsoft.VisualStudio.Web.CodeGeneration.Design')
$FrameworkFreezePrefixes = @('Microsoft.AspNetCore.','Microsoft.EntityFrameworkCore.','Microsoft.Extensions.','System.')
# GitVersion packages/tool are kept in sync with one another.
$GitVersionNuGetId = 'GitVersion.MsBuild'
$GitVersionToolId  = 'gitversion.tool'

# Source npm projects - discovered dynamically under src/, skipping generated/dependency
# copies (bin/obj/ctrl/.apax/node_modules/wwwroot). Override with -NpmProjects to be explicit.
function Get-NpmProjects {
    $srcRoot = Join-Path $repoRoot 'src'
    if(-not (Test-Path -LiteralPath $srcRoot)){ return @() }
    $excludeRx = '[\\/](bin|obj|ctrl|\.apax|node_modules|wwwroot|dist|\.git)[\\/]'
    Get-ChildItem -LiteralPath $srcRoot -Recurse -File -Filter 'package.json' -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -notmatch $excludeRx } |
        Select-Object -ExpandProperty FullName |
        Sort-Object
}

$NpmProjects = if($NpmProjects){ $NpmProjects | ForEach-Object { if([System.IO.Path]::IsPathRooted($_)){ $_ } else { Join-Path $repoRoot $_ } } }
              else { Get-NpmProjects }

# Accumulators for the report.
$Changes      = New-Object System.Collections.ArrayList   # applied/previewed bumps
$Frozen       = New-Object System.Collections.ArrayList   # id + reason
$Reverted     = New-Object System.Collections.ArrayList   # bumps removed by bisection
$ScanErrors   = New-Object System.Collections.ArrayList
$BuildResult  = 'not-run'

# Captured original file contents (for bisection replay). file path -> original raw text.
$OriginalContent = @{}

# ---------------------------------------------------------------------------
# Helpers
# ---------------------------------------------------------------------------
function Test-CommandAvailable { param([string]$Name) $null -ne (Get-Command $Name -ErrorAction SilentlyContinue) }

function Test-IsMajorBump {
    param([string]$From,[string]$To)
    $f = ConvertTo-VersionRecord $From
    $t = ConvertTo-VersionRecord $To
    return ($t.Major -gt $f.Major)
}

function Get-LatestStableVersion {
    # Highest non-prerelease version of $PackageId on the feed, or $null.
    param([string]$PackageId,$FeedCtx)
    $versions = @()
    try { $versions = Get-PackageVersionsFromFeed -PkgBase $FeedCtx.PkgBase -Headers $FeedCtx.Headers -PackageId $PackageId }
    catch { if($Detailed){ Write-Warn "feed lookup failed for ${PackageId}: $($_.Exception.Message)" }; return $null }
    if(-not $versions -or @($versions).Count -eq 0){ return $null }
    $stable = @($versions | Where-Object { -not (Test-IsPrerelease $_) })
    if($stable.Count -eq 0){ return $null }
    $best = $stable[0]
    foreach($v in $stable){ if(Test-VersionGreater $v $best){ $best = $v } }
    return $best
}

function Get-NuGetDisposition {
    # Returns 'update' or a 'freeze-*' / 'skip-*' reason for a package id.
    param([string]$Id)
    if($Id -match $AxSharpSkipPattern){ return 'skip-axsharp (owned by update_axsharp_versions.ps1)' }
    if($SecurityPinIds -contains $Id){ return 'skip-security-pin (owned by update-vulnerable-deps.ps1)' }
    if($FrameworkFreezeExact -contains $Id){ return 'freeze-framework (net10.0-tied)' }
    foreach($p in $FrameworkFreezePrefixes){ if($Id.StartsWith($p)){ return 'freeze-framework (net10.0-tied)' } }
    return 'update'
}

# ---------------------------------------------------------------------------
# Change application (replayable from captured original content)
# ---------------------------------------------------------------------------
function Apply-ChangeToContent {
    # Applies a single change to the supplied file content and returns the new content.
    param([string]$Content,$Change)
    $idEsc = [regex]::Escape($Change.Id)
    switch($Change.Kind){
        'props-packageversion' {
            $rx = "(<PackageVersion\b[^>]*\bInclude=`"$idEsc`"[^>]*\bVersion=`")[^`"]*(`")"
            return [regex]::Replace($Content,$rx,"`${1}$($Change.To)`${2}",1)
        }
        'props-globalref' {
            $rx = "(<GlobalPackageReference\b[^>]*\bInclude=`"$idEsc`"[^>]*\bVersion=`")[^`"]*(`")"
            return [regex]::Replace($Content,$rx,"`${1}$($Change.To)`${2}",1)
        }
        'tool' {
            $rx = "(`"$idEsc`"\s*:\s*\{[^{}]*?`"version`"\s*:\s*`")[^`"]+(`")"
            return [System.Text.RegularExpressions.Regex]::Replace(
                $Content,$rx,"`${1}$($Change.To)`${2}",
                [System.Text.RegularExpressions.RegexOptions]::Singleline)
        }
        'npm' {
            # Preserve any leading range operator (^, ~, >=, etc.) on the declared range.
            $rx = "(`"$idEsc`"\s*:\s*`")([^`"]*)(`")"
            return [regex]::Replace($Content,$rx,{
                param($m)
                $old = $m.Groups[2].Value
                $prefix = ''
                if($old -match '^([\^~>=<\s]*)'){ $prefix = $Matches[1] }
                $m.Groups[1].Value + $prefix + $Change.To + $m.Groups[3].Value
            },1)
        }
        default { return $Content }
    }
}

function Write-FileSet {
    # Rebuilds every affected file from its captured ORIGINAL content, applying only $ActiveChanges.
    # Returns the list of npm directories whose package.json changed (caller refreshes their lock).
    param($ActiveChanges)
    $byFile = $ActiveChanges | Group-Object -Property File
    $npmDirs = New-Object System.Collections.ArrayList
    foreach($g in $byFile){
        $file = $g.Name
        if(-not $OriginalContent.ContainsKey($file)){ continue }
        $content = $OriginalContent[$file]
        foreach($c in $g.Group){
            $content = Apply-ChangeToContent -Content $content -Change $c
            if($c.Ecosystem -eq 'npm'){ $dir = Split-Path -Parent $file; if(-not $npmDirs.Contains($dir)){ [void]$npmDirs.Add($dir) } }
        }
        Write-Utf8NoBom-LF -Path $file -Content $content
    }
    # Files that had ALL their changes removed must be restored to original too.
    foreach($file in $OriginalContent.Keys){
        if(-not ($byFile | Where-Object { $_.Name -eq $file })){
            Write-Utf8NoBom-LF -Path $file -Content $OriginalContent[$file]
        }
    }
    return $npmDirs
}

function Update-NpmLockfiles {
    param($NpmDirs)
    foreach($dir in $NpmDirs){
        if($Detailed){ Write-Info "  npm install (refresh lockfile): $dir" }
        Push-Location $dir
        try { & npm install --no-audit --no-fund *> $null }
        catch { [void]$ScanErrors.Add("npm install failed in ${dir}: $($_.Exception.Message)") }
        finally { Pop-Location }
    }
}

# ---------------------------------------------------------------------------
# Scanners (populate $Changes / $Frozen; capture originals)
# ---------------------------------------------------------------------------
function Invoke-NuGetScan {
    param($FeedCtx)
    Write-Info 'Scanning NuGet (Directory.Packages.props)...'
    if(-not (Test-Path $propsPath)){ [void]$ScanErrors.Add('Directory.Packages.props not found'); return }
    $content = Get-Content -LiteralPath $propsPath -Raw
    $OriginalContent[$propsPath] = $content

    $pvMatches = [regex]::Matches($content,'<PackageVersion\b[^>]*\bInclude="([^"]+)"[^>]*\bVersion="([^"]+)"')
    $grMatches = [regex]::Matches($content,'<GlobalPackageReference\b[^>]*\bInclude="([^"]+)"[^>]*\bVersion="([^"]+)"')

    foreach($m in @($pvMatches) + @($grMatches)){
        $id = $m.Groups[1].Value
        $cur = $m.Groups[2].Value
        $kind = if($m.Value -like '*GlobalPackageReference*'){ 'props-globalref' } else { 'props-packageversion' }

        # GitVersion handled together below.
        if($id -eq $GitVersionNuGetId){ continue }

        $disp = Get-NuGetDisposition $id
        if($disp -ne 'update'){ [void]$Frozen.Add([PSCustomObject]@{ Ecosystem='nuget'; Id=$id; Current=$cur; Reason=$disp }); continue }

        $latest = Get-LatestStableVersion -PackageId $id -FeedCtx $FeedCtx
        if(-not $latest){ [void]$Frozen.Add([PSCustomObject]@{ Ecosystem='nuget'; Id=$id; Current=$cur; Reason='no stable version on feed' }); continue }
        if(-not (Test-VersionGreater $latest $cur)){ if($Detailed){ Write-Info "  $id already latest ($cur)" }; continue }

        $isMajor = Test-IsMajorBump $cur $latest
        if($isMajor -and $SkipMajor){ [void]$Frozen.Add([PSCustomObject]@{ Ecosystem='nuget'; Id=$id; Current=$cur; Reason="major bump to $latest skipped (-SkipMajor)" }); continue }

        [void]$Changes.Add([PSCustomObject]@{ Ecosystem='nuget'; Id=$id; From=$cur; To=$latest; IsMajor=$isMajor; Kind=$kind; File=$propsPath })
        Write-Info ("  {0}: {1} -> {2}{3}" -f $id,$cur,$latest,$(if($isMajor){' [MAJOR]'}else{''}))
    }
}

function Resolve-GitVersionSync {
    # Resolves one latest-stable GitVersion across MsBuild pkg + dotnet tool and queues both bumps.
    param($FeedCtx)
    if(-not (Test-Path $propsPath)){ return }
    $propsRaw = $OriginalContent[$propsPath]
    if(-not $propsRaw){ $propsRaw = Get-Content -LiteralPath $propsPath -Raw; $OriginalContent[$propsPath] = $propsRaw }

    $curMsb = $null
    $m = [regex]::Match($propsRaw,"<GlobalPackageReference\b[^>]*\bInclude=`"$([regex]::Escape($GitVersionNuGetId))`"[^>]*\bVersion=`"([^`"]+)`"")
    if($m.Success){ $curMsb = $m.Groups[1].Value }

    $curTool = $null
    if(Test-Path $toolsPath){
        $toolsRaw = $OriginalContent[$toolsPath]
        if(-not $toolsRaw){ $toolsRaw = Get-Content -LiteralPath $toolsPath -Raw; $OriginalContent[$toolsPath] = $toolsRaw }
        $tm = [regex]::Match($toolsRaw,"`"$([regex]::Escape($GitVersionToolId))`"\s*:\s*\{[^{}]*?`"version`"\s*:\s*`"([^`"]+)`"",[System.Text.RegularExpressions.RegexOptions]::Singleline)
        if($tm.Success){ $curTool = $tm.Groups[1].Value }
    }

    $latestMsb  = Get-LatestStableVersion -PackageId $GitVersionNuGetId -FeedCtx $FeedCtx
    $latestTool = Get-LatestStableVersion -PackageId $GitVersionToolId  -FeedCtx $FeedCtx
    $synced = $latestMsb
    if($latestTool -and (Test-VersionGreater $latestTool $synced)){ $synced = $latestTool }
    if(-not $synced){ Write-Warn 'Could not resolve a latest stable GitVersion; leaving as-is.'; return }

    if($doNuget -and $curMsb -and (Test-VersionGreater $synced $curMsb)){
        $isMajor = Test-IsMajorBump $curMsb $synced
        if(-not ($isMajor -and $SkipMajor)){
            [void]$Changes.Add([PSCustomObject]@{ Ecosystem='nuget'; Id=$GitVersionNuGetId; From=$curMsb; To=$synced; IsMajor=$isMajor; Kind='props-globalref'; File=$propsPath })
            Write-Info ("  {0}: {1} -> {2} (GitVersion sync){3}" -f $GitVersionNuGetId,$curMsb,$synced,$(if($isMajor){' [MAJOR]'}else{''}))
        } else { [void]$Frozen.Add([PSCustomObject]@{ Ecosystem='nuget'; Id=$GitVersionNuGetId; Current=$curMsb; Reason="major bump to $synced skipped (-SkipMajor)" }) }
    }
    if($doTools -and $curTool -and (Test-VersionGreater $synced $curTool)){
        $isMajor = Test-IsMajorBump $curTool $synced
        if(-not ($isMajor -and $SkipMajor)){
            [void]$Changes.Add([PSCustomObject]@{ Ecosystem='tool'; Id=$GitVersionToolId; From=$curTool; To=$synced; IsMajor=$isMajor; Kind='tool'; File=$toolsPath })
            Write-Info ("  {0}: {1} -> {2} (GitVersion sync){3}" -f $GitVersionToolId,$curTool,$synced,$(if($isMajor){' [MAJOR]'}else{''}))
        } else { [void]$Frozen.Add([PSCustomObject]@{ Ecosystem='tool'; Id=$GitVersionToolId; Current=$curTool; Reason="major bump to $synced skipped (-SkipMajor)" }) }
    }
}

function Invoke-ToolsScan {
    param($FeedCtx)
    Write-Info 'Scanning dotnet tools (.config/dotnet-tools.json)...'
    if(-not (Test-Path $toolsPath)){ [void]$ScanErrors.Add('.config/dotnet-tools.json not found'); return }
    $raw = $OriginalContent[$toolsPath]
    if(-not $raw){ $raw = Get-Content -LiteralPath $toolsPath -Raw; $OriginalContent[$toolsPath] = $raw }
    try { $obj = $raw | ConvertFrom-Json -ErrorAction Stop } catch { [void]$ScanErrors.Add('tools JSON parse failed'); return }
    if(-not $obj.tools){ [void]$ScanErrors.Add("tools JSON missing 'tools'"); return }

    foreach($name in $obj.tools.PSObject.Properties.Name){
        if($name -like 'AXSharp.*' -or $name -like 'Inxton.Operon.*'){ [void]$Frozen.Add([PSCustomObject]@{ Ecosystem='tool'; Id=$name; Current=$obj.tools.$name.version; Reason='skip-axsharp (owned by update_axsharp_versions.ps1)' }); continue }
        if($name -eq $GitVersionToolId){ continue }   # handled by Resolve-GitVersionSync
        $cur = $obj.tools.$name.version
        $latest = Get-LatestStableVersion -PackageId $name -FeedCtx $FeedCtx
        if(-not $latest){ [void]$Frozen.Add([PSCustomObject]@{ Ecosystem='tool'; Id=$name; Current=$cur; Reason='no stable version on feed' }); continue }
        if(-not (Test-VersionGreater $latest $cur)){ if($Detailed){ Write-Info "  $name already latest ($cur)" }; continue }
        $isMajor = Test-IsMajorBump $cur $latest
        if($isMajor -and $SkipMajor){ [void]$Frozen.Add([PSCustomObject]@{ Ecosystem='tool'; Id=$name; Current=$cur; Reason="major bump to $latest skipped (-SkipMajor)" }); continue }
        [void]$Changes.Add([PSCustomObject]@{ Ecosystem='tool'; Id=$name; From=$cur; To=$latest; IsMajor=$isMajor; Kind='tool'; File=$toolsPath })
        Write-Info ("  {0}: {1} -> {2}{3}" -f $name,$cur,$latest,$(if($isMajor){' [MAJOR]'}else{''}))
    }
}

function Invoke-NpmScan {
    Write-Info 'Scanning npm projects...'
    if(-not (Test-CommandAvailable 'npm')){ Write-Err 'npm not found on PATH.'; [void]$ScanErrors.Add('npm not found'); return }
    foreach($pkgJson in $NpmProjects){
        if(-not (Test-Path -LiteralPath $pkgJson)){ if($Detailed){ Write-Warn "missing: $pkgJson" }; continue }
        $dir  = Split-Path -Parent $pkgJson
        $name = (Resolve-Path -LiteralPath $dir).Path.Replace((Resolve-Path $repoRoot).Path,'').TrimStart('\','/')
        Write-Info "  $name"
        $OriginalContent[$pkgJson] = Get-Content -LiteralPath $pkgJson -Raw
        Push-Location $dir
        try {
            if(-not (Test-Path 'node_modules')){
                if($Detailed){ Write-Info "    npm install (no node_modules)" }
                & npm install --no-audit --no-fund *> $null
            }
            $raw = & npm outdated --json 2>$null | Out-String
            if(-not $raw.Trim()){ if($Detailed){ Write-Info "    up to date" }; Pop-Location; continue }
            $parsed = $null; try { $parsed = $raw | ConvertFrom-Json } catch { [void]$ScanErrors.Add("npm outdated unparseable: $name"); Pop-Location; continue }
            if(-not $parsed){ Pop-Location; continue }
            foreach($p in $parsed.PSObject.Properties){
                $pkgName = $p.Name
                $info = $p.Value
                if($info -is [System.Object[]]){ $info = $info[0] }   # multiple entries -> take first
                $cur = if($info.PSObject.Properties.Name -contains 'current'){ $info.current } else { $null }
                $latest = if($info.PSObject.Properties.Name -contains 'latest'){ $info.latest } else { $null }
                if(-not $cur -or -not $latest){ continue }              # not installed / no candidate
                if(Test-IsPrerelease $latest){ continue }               # stable only
                if(-not (Test-VersionGreater $latest $cur)){ continue }
                $isMajor = Test-IsMajorBump $cur $latest
                if($isMajor -and $SkipMajor){ [void]$Frozen.Add([PSCustomObject]@{ Ecosystem='npm'; Id="$name/$pkgName"; Current=$cur; Reason="major bump to $latest skipped (-SkipMajor)" }); continue }
                [void]$Changes.Add([PSCustomObject]@{ Ecosystem='npm'; Id=$pkgName; Project=$name; From=$cur; To=$latest; IsMajor=$isMajor; Kind='npm'; File=$pkgJson })
                Write-Info ("    {0}: {1} -> {2}{3}" -f $pkgName,$cur,$latest,$(if($isMajor){' [MAJOR]'}else{''}))
            }
        } catch { [void]$ScanErrors.Add("npm scan error in ${name}: $($_.Exception.Message)") }
        finally { Pop-Location }
    }
}

# ---------------------------------------------------------------------------
# Build verification + bisection
# ---------------------------------------------------------------------------
function Invoke-CakeBuild {
    if(-not (Test-CommandAvailable 'dotnet')){ Write-Err 'dotnet CLI not found; cannot build.'; return $false }
    Write-Info 'Running full cake build (dotnet run --project cake/Build.csproj) ...'
    & dotnet run --project $cakeProj
    return ($LASTEXITCODE -eq 0)
}

function Invoke-BisectGreenSubset {
    # ddmin-style: remove minimal change subsets until the build is green, bounded by $MaxBisectBuilds.
    # Mutates the working tree to the green subset; records removed changes in $script:Reverted.
    $builds = 0
    $active = @($Changes)
    $n = 2
    Write-Warn "Build failed with all $($active.Count) change(s). Bisecting for a green subset (max $MaxBisectBuilds builds)..."
    while($builds -lt $MaxBisectBuilds -and $active.Count -gt 1){
        $chunkSize = [Math]::Ceiling($active.Count / $n)
        $removedSomething = $false
        for($i=0; $i -lt $active.Count; $i += $chunkSize){
            $chunk = $active[$i..([Math]::Min($i+$chunkSize-1,$active.Count-1))]
            $trial = $active | Where-Object { $chunk -notcontains $_ }
            if(@($trial).Count -eq 0){ continue }
            $dirs = Write-FileSet -ActiveChanges $trial
            Update-NpmLockfiles -NpmDirs $dirs
            $builds++
            Write-Info "  bisect build $builds/$MaxBisectBuilds : trying $((@($trial)).Count) of $($active.Count) changes"
            if(Invoke-CakeBuild){ $active = @($trial); $n = [Math]::Max($n-1,2); $removedSomething = $true; break }
            if($builds -ge $MaxBisectBuilds){ break }
        }
        if(-not $removedSomething){
            if($n -ge $active.Count){ break }
            $n = [Math]::Min($n*2,$active.Count)
        }
    }
    # Record what got dropped and lock in the surviving subset.
    $kept = @($active)
    foreach($c in $Changes){ if($kept -notcontains $c){ [void]$Reverted.Add($c) } }
    $dirs = Write-FileSet -ActiveChanges $kept
    Update-NpmLockfiles -NpmDirs $dirs
    if($kept.Count -gt 0 -and $builds -lt $MaxBisectBuilds){
        $builds++
        Write-Info "  bisect build $builds : confirming surviving subset ($($kept.Count) change(s))"
        $script:BuildResult = if(Invoke-CakeBuild){ 'pass (after bisection)' } else { 'FAIL (bisection exhausted - manual review)' }
    } else {
        $script:BuildResult = "indeterminate (bisection hit -MaxBisectBuilds=$MaxBisectBuilds)"
    }
}

# ---------------------------------------------------------------------------
# Report
# ---------------------------------------------------------------------------
function Write-Report {
    param([string]$Stamp)
    $reportsDir = Join-Path $scriptRoot 'reports'
    if(-not (Test-Path $reportsDir)){ New-Item -ItemType Directory -Path $reportsDir -Force | Out-Null }
    $mdPath   = Join-Path $reportsDir "latest-deps-report-$Stamp.md"
    $jsonPath = Join-Path $reportsDir "latest-deps-report-$Stamp.json"

    $applied = @($Changes | Where-Object { $Reverted -notcontains $_ })
    $majors  = @($applied | Where-Object { $_.IsMajor })

    $payload = [PSCustomObject]@{
        timestamp  = $Stamp
        dryRun     = [bool]$DryRun
        skipMajor  = [bool]$SkipMajor
        ecosystems = @{ nuget=[bool]$doNuget; npm=[bool]$doNpm; tools=[bool]$doTools }
        build      = $BuildResult
        applied    = $applied
        reverted   = $Reverted
        frozen     = $Frozen
        majors     = $majors
        scanErrors = $ScanErrors
    }
    Write-Utf8NoBom-LF -Path $jsonPath -Content ($payload | ConvertTo-Json -Depth 8)

    $sb = New-Object System.Text.StringBuilder
    [void]$sb.AppendLine('# Latest-dependency update report')
    [void]$sb.AppendLine('')
    [void]$sb.AppendLine("- Generated: $Stamp")
    [void]$sb.AppendLine("- Dry run: $([bool]$DryRun)")
    [void]$sb.AppendLine("- Skip major: $([bool]$SkipMajor)")
    [void]$sb.AppendLine("- Ecosystems: NuGet=$doNuget, npm=$doNpm, tools=$doTools")
    [void]$sb.AppendLine("- Build result: **$BuildResult**")
    [void]$sb.AppendLine('')

    if($majors.Count -gt 0){
        [void]$sb.AppendLine("## [!] Major-version bumps ($($majors.Count)) - review carefully")
        [void]$sb.AppendLine('| Ecosystem | Package | From | To |')
        [void]$sb.AppendLine('|---|---|---|---|')
        foreach($c in $majors){ [void]$sb.AppendLine("| $($c.Ecosystem) | $($c.Id) | $($c.From) | $($c.To) |") }
        [void]$sb.AppendLine('')
    }

    [void]$sb.AppendLine("## NuGet bumped ($(@($applied | Where-Object { $_.Ecosystem -eq 'nuget' }).Count))")
    [void]$sb.AppendLine('| Package | From | To | Major? |')
    [void]$sb.AppendLine('|---|---|---|---|')
    foreach($c in ($applied | Where-Object { $_.Ecosystem -eq 'nuget' })){ [void]$sb.AppendLine("| $($c.Id) | $($c.From) | $($c.To) | $(if($c.IsMajor){'YES'}else{''}) |") }
    [void]$sb.AppendLine('')

    [void]$sb.AppendLine("## npm bumped ($(@($applied | Where-Object { $_.Ecosystem -eq 'npm' }).Count))")
    [void]$sb.AppendLine('| Project | Package | From | To | Major? |')
    [void]$sb.AppendLine('|---|---|---|---|---|')
    foreach($c in ($applied | Where-Object { $_.Ecosystem -eq 'npm' })){ [void]$sb.AppendLine("| $($c.Project) | $($c.Id) | $($c.From) | $($c.To) | $(if($c.IsMajor){'YES'}else{''}) |") }
    [void]$sb.AppendLine('')

    [void]$sb.AppendLine("## Tools bumped ($(@($applied | Where-Object { $_.Ecosystem -eq 'tool' }).Count))")
    [void]$sb.AppendLine('| Tool | From | To |')
    [void]$sb.AppendLine('|---|---|---|')
    foreach($c in ($applied | Where-Object { $_.Ecosystem -eq 'tool' })){ [void]$sb.AppendLine("| $($c.Id) | $($c.From) | $($c.To) |") }
    [void]$sb.AppendLine('')

    if($Reverted.Count -gt 0){
        [void]$sb.AppendLine("## Reverted by build bisection ($($Reverted.Count))")
        [void]$sb.AppendLine('| Ecosystem | Package | From | To |')
        [void]$sb.AppendLine('|---|---|---|---|')
        foreach($c in $Reverted){ [void]$sb.AppendLine("| $($c.Ecosystem) | $($c.Id) | $($c.From) | $($c.To) |") }
        [void]$sb.AppendLine('')
    }

    [void]$sb.AppendLine("## Frozen / skipped ($($Frozen.Count))")
    [void]$sb.AppendLine('| Ecosystem | Package | Current | Reason |')
    [void]$sb.AppendLine('|---|---|---|---|')
    foreach($f in $Frozen){ [void]$sb.AppendLine("| $($f.Ecosystem) | $($f.Id) | $($f.Current) | $($f.Reason) |") }
    [void]$sb.AppendLine('')

    if($ScanErrors.Count -gt 0){
        [void]$sb.AppendLine('## Scan errors')
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

    $branch = 'chore/update-latest-deps'
    Write-Info "Creating branch '$branch' off origin/dev and opening PR..."

    & git -C $repoRoot fetch origin dev --quiet
    & git -C $repoRoot stash push -u -m 'latest-deps-wip' *> $null
    $stashed = ($LASTEXITCODE -eq 0)
    & git -C $repoRoot switch -C $branch origin/dev
    if($LASTEXITCODE -ne 0){ Write-Err "Failed to create branch $branch."; if($stashed){ & git -C $repoRoot stash pop *> $null }; return }
    if($stashed){
        & git -C $repoRoot stash pop
        if($LASTEXITCODE -ne 0){ Write-Err 'Stash pop conflicted; resolve manually. Aborting PR.'; return }
    }

    & git -C $repoRoot add -- 'Directory.Packages.props' '.config/dotnet-tools.json' '**/package.json' '**/package-lock.json'
    & git -C $repoRoot commit -m @'
chore(deps): update dependencies to latest stable

Automated by scripts/update-latest-deps.ps1.

Co-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>
'@
    if($LASTEXITCODE -ne 0){ Write-Warn 'Nothing to commit (no changes staged). Skipping PR.'; return }

    & git -C $repoRoot push -u origin $branch
    if($LASTEXITCODE -ne 0){ Write-Err 'git push failed.'; return }

    $applied   = @($Changes | Where-Object { $Reverted -notcontains $_ })
    $bumpLines = ($applied | ForEach-Object { "- $($_.Ecosystem) $($_.Id): $($_.From) -> $($_.To)$(if($_.IsMajor){' (MAJOR)'}else{''})" }) -join "`n"
    $majorList = ($applied | Where-Object { $_.IsMajor } | ForEach-Object { "- $($_.Ecosystem) $($_.Id): $($_.From) -> $($_.To)" }) -join "`n"
    $revLines  = ($Reverted | ForEach-Object { "- $($_.Ecosystem) $($_.Id): $($_.From) -> $($_.To)" }) -join "`n"
    $body = @"
Automated update of (non-AXSharp) dependencies to their latest stable versions.

Build verification: $BuildResult

## Bumped ($($applied.Count))
$bumpLines

## [!] Major-version bumps
$(if($majorList){ $majorList } else { '_none_' })

## Reverted by build bisection
$(if($revLines){ $revLines } else { '_none_' })

See the attached report ($([System.IO.Path]::GetFileName($ReportPath))) for frozen/skipped detail.

🤖 Generated with [Claude Code](https://claude.com/claude-code)
"@
    & gh pr create --base dev --head $branch --title 'chore(deps): update dependencies to latest stable' --body $body
    if($LASTEXITCODE -ne 0){ Write-Err 'gh pr create failed.' } else { Write-Info 'PR opened against dev.' }
}

# ---------------------------------------------------------------------------
# Main
# ---------------------------------------------------------------------------
$stamp = (Get-Date).ToString('yyyy-MM-dd-HHmmss')
Write-Info "update-latest-deps  (apply=$Apply, dryRun=$DryRun, nuget=$doNuget, npm=$doNpm, tools=$doTools, skipMajor=$SkipMajor)"
if($DryRun){ Write-Warn 'DRY RUN - no files will be changed. Pass -Apply to write.' }

$feedCtx = $null
if($doNuget -or $doTools){
    try { $feedCtx = Get-FeedContext -Feed $Source -User $null -Tok $feedToken } catch { Write-Err "Could not initialise NuGet feed: $($_.Exception.Message)"; exit 2 }
}

if($doNuget){ Invoke-NuGetScan -FeedCtx $feedCtx }
if($doTools){ Invoke-ToolsScan -FeedCtx $feedCtx }
if($doNuget -or $doTools){ Resolve-GitVersionSync -FeedCtx $feedCtx }
if($doNpm){ Invoke-NpmScan }

Write-Host ''
Write-Host '================ PLAN ================' -ForegroundColor Cyan
Write-Host ("  Changes : {0}" -f $Changes.Count)
Write-Host ("  Major   : {0}" -f @($Changes | Where-Object { $_.IsMajor }).Count)
Write-Host ("  Frozen  : {0}" -f $Frozen.Count)
Write-Host ("  Errors  : {0}" -f $ScanErrors.Count)
Write-Host '=====================================' -ForegroundColor Cyan

if($DryRun){
    Write-Warn '[DRY RUN] No changes written. Review the report below.'
    $reportPath = Write-Report -Stamp $stamp
    if($ScanErrors.Count -gt 0){ Write-Warn "$($ScanErrors.Count) scan error(s) - see report." }
    exit 0
}

if($Changes.Count -eq 0){
    Write-Info 'Nothing to update; everything already at latest stable.'
    Write-Report -Stamp $stamp | Out-Null
    exit 0
}

# Apply everything, then verify.
$dirs = Write-FileSet -ActiveChanges @($Changes)
Update-NpmLockfiles -NpmDirs $dirs
Write-Info "Applied $($Changes.Count) change(s) to the working tree."

if(-not $SkipBuild){
    if(Invoke-CakeBuild){
        $BuildResult = 'pass'
        Write-Info 'Build passed with all changes.'
    } elseif($RollbackAllOnFailure){
        Write-Warn 'Build failed; -RollbackAllOnFailure set, reverting everything.'
        foreach($c in $Changes){ [void]$Reverted.Add($c) }
        Write-FileSet -ActiveChanges @() | Out-Null
        $BuildResult = 'FAIL (all changes rolled back)'
    } else {
        Invoke-BisectGreenSubset
    }
} else {
    $BuildResult = 'skipped (-SkipBuild)'
    Write-Warn 'Build verification skipped (-SkipBuild).'
}

$reportPath = Write-Report -Stamp $stamp

Write-Host ''
Write-Host '================ SUMMARY ================' -ForegroundColor Cyan
Write-Host ("  Applied  : {0}" -f @($Changes | Where-Object { $Reverted -notcontains $_ }).Count)
Write-Host ("  Reverted : {0}" -f $Reverted.Count)
Write-Host ("  Frozen   : {0}" -f $Frozen.Count)
Write-Host ("  Build    : {0}" -f $BuildResult)
Write-Host ("  Errors   : {0}" -f $ScanErrors.Count)
Write-Host '========================================' -ForegroundColor Cyan

if($CreatePR){
    if(@($Changes | Where-Object { $Reverted -notcontains $_ }).Count -gt 0){ Invoke-CreatePR -ReportPath $reportPath }
    else { Write-Warn 'No surviving changes; skipping PR creation.' }
}

if($BuildResult -like 'FAIL*'){ exit 1 }
exit 0
