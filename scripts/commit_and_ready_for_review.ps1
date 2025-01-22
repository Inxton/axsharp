if (-not $args[0]) 
{
    $commitMessage = Read-Host "Please provide a commit message." 
}
else
{
    $commitMessage = $args[0]
}
git add .
git commit -m "$commitMessage"
git push
# Get the current script directory
$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
# Construct the full path to ready_for_review.ps1
$ready_for_reviewScriptPath = Join-Path -Path $scriptDir -ChildPath "ready_for_review.ps1"
& $ready_for_reviewScriptPath
