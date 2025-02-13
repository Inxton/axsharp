if (-not $args[0]) 
{
    $commitMessage = Read-Host "Please provide a commit message." 
}
else
{
    $commitMessage = $args[0]
}
# Get the current script directory
$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
$parentDir = Resolve-Path -Path "$scriptDir\.."
# without this probably just the content of the script folder is added to the commit 
cd $parentDir 
git add .
git commit -m "$commitMessage"
git push
# Construct the full path to ready_for_review.ps1
$ready_for_reviewScriptPath = Join-Path -Path $scriptDir -ChildPath "ready_for_review.ps1"
& $ready_for_reviewScriptPath
