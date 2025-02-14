$currentBranch = git branch --show-current
if($currentBranch -ne "dev")
{
    Write-Output "You are not currently on the 'dev' branch."
    Write-Host "You are not currently on the 'dev' branch, but $currentBranch " -ForegroundColor Red
    return $false
}
else
{
    Write-Host "You are currently on the 'dev' branch" -ForegroundColor Green
}
$isClean =$false
$status = git status
foreach ($statusLine in $status ) 
{
    if ($statusLine -eq "nothing to commit, working tree clean") 
    {
        $isClean = $true
        Write-Host "Nothing to commit, working tree clean" -ForegroundColor Green
        break
    }
}
if(-not $isClean)
{
    Write-Host "You have some uncommited changes on the 'dev' branch" -ForegroundColor Red
}
return $isClean