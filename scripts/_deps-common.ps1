#!/usr/bin/env pwsh
<#
.SYNOPSIS
Shared helpers for dependency-management scripts (dot-source this file).

.DESCRIPTION
Common logging, file IO, semver comparison, NuGet v3 feed access, token discovery and
severity utilities used by scripts/update-vulnerable-deps.ps1 (and reusable by
scripts/update_axsharp_versions.ps1). Several functions here originated in
update_axsharp_versions.ps1 and were factored out so the two scripts share one source of truth.

Dot-source it from a script:
    . "$PSScriptRoot/_deps-common.ps1"
#>

# ---------------------------------------------------------------------------
# Logging
# ---------------------------------------------------------------------------
function Write-Info($msg){ Write-Host "[INFO ] $msg" -ForegroundColor Cyan }
function Write-Warn($msg){ Write-Host "[WARN ] $msg" -ForegroundColor Yellow }
function Write-Err ($msg){ Write-Host "[ERROR] $msg" -ForegroundColor Red }

# ---------------------------------------------------------------------------
# File IO
# ---------------------------------------------------------------------------
function Write-Utf8NoBom-LF {
    # Writes text as UTF-8 (no BOM) with LF line endings to preserve repo conventions.
    param([string]$Path, [string]$Content)
    $lf = ($Content -replace "`r`n", "`n") -replace "`r", "`n"
    [System.IO.File]::WriteAllText($Path, $lf, [System.Text.UTF8Encoding]::new($false))
}

# ---------------------------------------------------------------------------
# Token discovery
# ---------------------------------------------------------------------------
function Resolve-FeedToken {
    # Returns the first non-empty token from the supplied value or the candidate env vars.
    param(
        [string]$Token,
        [string[]]$EnvCandidates = @('NUGET_TOKEN','GITHUB_PACKAGES_TOKEN','GITHUB_TOKEN','GH_TOKEN'),
        [switch]$Detailed
    )
    if($Token){ return $Token }
    foreach($c in $EnvCandidates){
        $candidate = [Environment]::GetEnvironmentVariable($c)
        if($candidate){
            if($Detailed){ Write-Info "Using token from environment variable $c" }
            return $candidate
        }
    }
    return $null
}

# ---------------------------------------------------------------------------
# Semver parsing / comparison (build metadata ignored for ordering)
# ---------------------------------------------------------------------------
function ConvertTo-VersionRecord {
    param([string]$v)
    $core = $v
    $pre = ''
    $buildSplit = $core.Split('+',2)
    if($buildSplit.Count -gt 1){ $core = $buildSplit[0] }
    $dashIdx = $core.IndexOf('-')
    if($dashIdx -ge 0){
        $pre = $core.Substring($dashIdx + 1)
        $core = $core.Substring(0,$dashIdx)
    }
    $parts = $core.Split('.')
    [int]$maj = if($parts.Count -gt 0){ $parts[0] } else { 0 }
    [int]$min = if($parts.Count -gt 1){ $parts[1] } else { 0 }
    [int]$pat = if($parts.Count -gt 2){ $parts[2] } else { 0 }
    $preSegs = @()
    if($pre){ $preSegs = $pre.Split('.') }
    [PSCustomObject]@{ Original=$v; Major=$maj; Minor=$min; Patch=$pat; Pre=$pre; PreSegs=$preSegs }
}

function Compare-VersionRecord {
    param($a,$b)
    if($a.Major -ne $b.Major){ return [Math]::Sign($a.Major - $b.Major) }
    if($a.Minor -ne $b.Minor){ return [Math]::Sign($a.Minor - $b.Minor) }
    if($a.Patch -ne $b.Patch){ return [Math]::Sign($a.Patch - $b.Patch) }
    $aHasPre = [string]::IsNullOrEmpty($a.Pre) -ne $true
    $bHasPre = [string]::IsNullOrEmpty($b.Pre) -ne $true
    if($aHasPre -and -not $bHasPre){ return -1 }
    if($bHasPre -and -not $aHasPre){ return 1 }
    if(-not $aHasPre -and -not $bHasPre){ return 0 }
    $len = [Math]::Max($a.PreSegs.Count,$b.PreSegs.Count)
    for($i=0;$i -lt $len;$i++){
        if($i -ge $a.PreSegs.Count){ return -1 }
        if($i -ge $b.PreSegs.Count){ return 1 }
        $as = $a.PreSegs[$i]; $bs = $b.PreSegs[$i]
        $aNum = $as -as [int]; $bNum = $bs -as [int]
        $aIsNum = $aNum -ne $null; $bIsNum = $bNum -ne $null
        if($aIsNum -and $bIsNum){ if($aNum -ne $bNum){ return [Math]::Sign($aNum - $bNum) } }
        elseif($aIsNum -and -not $bIsNum){ return -1 }
        elseif($bIsNum -and -not $aIsNum){ return 1 }
        else { $cmp = [string]::Compare($as,$bs,$true); if($cmp -ne 0){ return [Math]::Sign($cmp) } }
    }
    return 0
}

function Test-VersionGreater {
    # Returns $true when semver-ish version $A is strictly greater than $B.
    param([string]$A,[string]$B)
    if([string]::IsNullOrWhiteSpace($B)){ return $true }
    if([string]::IsNullOrWhiteSpace($A)){ return $false }
    return ( (Compare-VersionRecord (ConvertTo-VersionRecord $A) (ConvertTo-VersionRecord $B)) -gt 0 )
}

function Test-IsPrerelease {
    param([string]$v)
    if([string]::IsNullOrWhiteSpace($v)){ return $false }
    return -not [string]::IsNullOrEmpty((ConvertTo-VersionRecord $v).Pre)
}

# ---------------------------------------------------------------------------
# NuGet v3 feed access
# ---------------------------------------------------------------------------
function Get-FeedContext {
    # Resolves the service index once and returns the PackageBaseAddress + auth headers
    # for reuse across multiple package requests.
    param([string]$Feed,[string]$User,[string]$Tok)
    $serviceIndexUrl = if($Feed.ToLower().EndsWith('index.json')) { $Feed } else { ($Feed.TrimEnd('/')) + '/index.json' }
    $headers = @{}
    if($Tok){
        $u = if($User){$User}else{'USERNAME'}
        $basic = [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(("{0}:{1}" -f $u,$Tok)))
        $headers['Authorization'] = "Basic $basic"
    }
    $si = Invoke-RestMethod -Uri $serviceIndexUrl -Headers $headers -TimeoutSec 30
    if(-not $si.resources){ throw "Service index missing resources at $serviceIndexUrl" }
    $pkgBase = ($si.resources | Where-Object { $_.'@type' -eq 'PackageBaseAddress/3.0.0' } | Select-Object -First 1).'@id'
    if(-not $pkgBase){ throw 'PackageBaseAddress/3.0.0 resource not found in service index.' }
    if($pkgBase[-1] -ne '/') { $pkgBase += '/' }
    [PSCustomObject]@{ PkgBase=$pkgBase; Headers=$headers }
}

function Get-PackageVersionsFromFeed {
    # Returns the raw list of available version strings for a package id on a v3 feed.
    param([string]$PkgBase,[hashtable]$Headers,[string]$PackageId)
    $lowerId = $PackageId.ToLower()
    $indexUrl = "$PkgBase$lowerId/index.json"
    try {
        $idx = Invoke-RestMethod -Uri $indexUrl -Headers $Headers -TimeoutSec 30
    } catch {
        $code = $null
        if($_.Exception.Response){ $code = [int]$_.Exception.Response.StatusCode }
        if($code -eq 404){ return @() }  # package id unknown on this feed
        throw "Failed to fetch version index ($indexUrl): $($_.Exception.Message)"
    }
    if(-not $idx.versions){ return @() }
    return $idx.versions
}
