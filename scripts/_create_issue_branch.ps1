param (
    [int]$IssueId )

gh issue list --assignee "@me" --state "open"
$issues = gh issue list --state "open" --assignee "@me" --json number,title | ConvertFrom-Json
$issueIDs = $issues | ForEach-Object { $_.number }

if (-not $IssueId) 
{
    $IssueId = Read-Host "Please enter an ID value of the issue"
}


if ([int]::TryParse($IssueId, [ref]$null)) 
{
    # Check if any of the open issue IDs assigned to @me is equal to entered value
    if ($issueIDs -contains [int]$IssueId) 
    {
        # Get selected issue id and issue title
        $selectedIssue = $issues | Where-Object { $_.number -eq [int]$IssueId }
        $selectedIssueNumber = $selectedIssue.number
        $selectedIssueTitle = $selectedIssue.title
        # Checkout dev
        Write-Output "Checkout to dev"
        git checkout dev
        $currentBranch = git branch --show-current
        if($currentBranch -eq "dev")
        {
            # Get remote changes, if any
            git pull
            # Create branch for the selected issue
            Write-Output "Creating branch for the issue number: '$selectedIssueNumber', title: '$selectedIssueTitle'"
            gh issue develop $IssueId --base dev --checkout
            # Add all changes 
            git add .
            # Commit all changes 
            git commit --allow-empty -m "Create draft PR for #$selectedIssueNumber"
            # Write changes to remote            
            Write-Output "Pushing the branch to remote"
            git push -u origin $(git branch --show-current)
            # Create draft PR
            Write-Output "Creating a draft pull request into 'dev'"
            gh pr create --base dev --head $(git branch --show-current) --title "$selectedIssueTitle" --body "closes #$selectedIssueNumber" --draft
            # Sync
            git push 
            Write-Output "Sync local and remote branches"
            git pull origin $(git branch --show-current)
            git push 
        } else 
        {
            Write-Output "Unable to checkout to dev"
            Write-Output "Commit your local changes, sync your local 'dev' branch with th remote and start this script again."
            exit 1
        }
    } 
    else {
        Write-Output "Error: The issue ID '$IssueId' does not exist in the list of open issues."
        exit 1
    }
} else {
    Write-Output "Error: The value '$IssueId' is not a valid numeric value."
    exit 1
}