param 
(
    [string]$IssueTitle, 
    [string]$IssueBody 
)

if (-not $IssueTitle) 
{
    $IssueTitle = Read-Host "Please enter an issue title."
}
if (-not $IssueBody) 
{
    $IssueBody = Read-Host "Please enter an issue body."
}


if (-not $IssueTitle) 
{
    Write-Output "Issue title cannot be an empty string!"
    exit 1
}
if (-not $IssueBody) 
{
    $IssueBody = " "
}


$issue = gh issue create --assignee "@me" --title "$IssueTitle" --body "$IssueBody" 
#--project simatic-ax

if ($issue -match ".*/(\d+)$") {
    $issueID = $matches[1] 
    Write-Output "The issue ID is: $issueID"
    # Get the current script directory
    $scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

    # Construct the full path to _create_issue_branch.ps1
    $_create_issue_branchScriptPath = Join-Path -Path $scriptDir -ChildPath "_create_issue_branch.ps1"

    # Call _create_issue_branch.ps1 with the parameter IssueId and doNotCheckOldColumnName
    & $_create_issue_branchScriptPath -IssueId $issueID -doNotCheckOldColumnName $True
} else {
    Write-Output "No numeric ID found for this issue."
}


