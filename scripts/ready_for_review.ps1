$gitStatus = git status
if($gitStatus -notcontains "nothing to commit, working tree clean")
{
    Write-Output "Working tree is not clean. Commit and push your local changes and run this script again afterwards."
}
else
{
    # Get current branch name
    $currentBranch = $(git branch --show-current)
    Write-Output "Working tree is clean. Marking '$currentBranch' as ready for review"
    # Extract the issue number (assuming "issue-<number>" or "<type>/<number>-description" format)
    if ($currentBranch -match "\d+") 
    {
        $issueID = $matches[0]
        Write-Output "issueID: $issueID"
        # Get the current script directory
        $scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
        # Construct the full path to _change_stateScriptPath.ps1
        $_change_stateScriptPath = Join-Path -Path $scriptDir -ChildPath "_change_state.ps1"
        # Call _change_state.ps1 with the parameters IssueId, oldColumnName, newColumnName,doNotCheckOldColumnName, repoOwner, repoName, projectName
        & $_change_stateScriptPath -IssueId $issueID -oldColumnName "In progress" -newColumnName "In review" -doNotCheckOldColumnName $False -repoOwner "Inxton" -repoName "AXOpen" -projectName "simatic-ax"
    } 
    else 
    {
        Write-Output "No issue number found in the branch name. Unable to move the issue to 'In review'"
    }

    # Get the list of pull requests
    $pullRequests = gh pr list --state open --json number,headRefName | ConvertFrom-Json
    if (-not $pullRequests) {
        Write-Error "No open pull requests found or an error occurred while retrieving them."
        exit 1
    }
    # Find the pull request matching the current branch
    $pullRequest = $pullRequests | Where-Object { $_.headRefName -eq $currentBranch }
    if (-not $pullRequest) 
    {
        Write-Output "No pull request found for the current branch ($currentBranch)."
        exit 1
    }
    $pullRequestNumber = $pullRequest.number
    Write-Output "Pull Request Number: $($pullRequestNumber)"
    # Assign it to me
    gh pr edit $pullRequestNumber --add-assignee "@me"
    Write-Host "Please enter the additional reviewers names: i.e.: my_boss_github_name, his_boss_github_name, etc. (upto the galaxy owner)"
    Write-Host "If environment variable 'GH_REVIEWERS' exists, these names will be added, otherwise these names will be used as the only reviewers"
    $AdditionalReviewers = Read-Host "For empty press enter."
    $ReviewComment = Read-Host "Please enter a comment for the reviewers, for empty press enter"
    Write-Output "currentBranch:  $currentBranch"
    if ($Env:GH_REVIEWERS -ne $null) 
    {
        if (-not $AdditionalReviewers) 
        {
            $Reviewers = $Env:GH_REVIEWERS 
        }
        else
        {
            $Reviewers = $Env:GH_REVIEWERS + "," + $AdditionalReviewers
        }
    } 
    else 
    {
        $Reviewers = $AdditionalReviewers
    }
    if (-not $Reviewers) 
    {
        Write-Error "No reviewers defined."
        exit 1
    }
    Write-Output "Reviewers:  $Reviewers"
    $ReviewersArray = $Reviewers -split "," | ForEach-Object { $_.Trim() }
    $ReviewersString = $ReviewersArray -join ","
    $Command = "gh pr edit --add-reviewer " + $ReviewersString
    Invoke-Expression $Command
    if ($ReviewComment -ne "") {
        $Command = "gh pr comment $pullRequestNumber -b """ + $ReviewComment + """"
        Invoke-Expression $Command
    }
    gh pr ready $pullRequestNumber
}

