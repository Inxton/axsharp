param (
    [int]$IssueId,
    [string]$oldColumnName,
    [string]$newColumnName,
    [bool]$doNotCheckOldColumnName = 0,
    [string]$repoOwner = "Inxton",
    [string]$repoName = "axsharp",
    [string]$projectName = "simatic-ax"
)

gh issue list --assignee "@me" --state "open"
$issues = gh issue list --state "open" --assignee "@me" --json number,title | ConvertFrom-Json
$issueIDs = $issues | ForEach-Object { $_.number }
if (-not $IssueId) 
{
    $IssueId = Read-Host "Please enter an ID value of the issue"
}

# Fetch project IDs using GraphQL
$projectIds = gh api graphql -f query='
query($repoOwner: String!, $repoName: String!) {
  repository(owner: $repoOwner, name: $repoName) {
    projectsV2(first: 10) {
      nodes {
        id
        title
      }
    }
  }
}' -f repoOwner=$repoOwner -F repoName=$repoName

$projectId = ($projectIds | ConvertFrom-Json).data.repository.projectsV2.nodes | Where-Object { $_.title -eq $projectName } | Select-Object -ExpandProperty id

$projectOptions = gh api graphql -f query='
query($projectId: ID!) {
  node(id: $projectId) {
    ... on ProjectV2 {
      fields(first: 20) {
        nodes {
          ... on ProjectV2SingleSelectField {
            id
            name
            options {
              id
              name
            }
          }
 		}
      }
    }
  }
}' -f projectId=$projectId | ConvertFrom-Json

$projectColumns = $projectOptions.data.node.fields.nodes | Where-Object { $_.name -eq 'Status'}
$projectColumnId =$projectColumns.id
if(-not $projectColumnId)
{
    Write-Output "Error: No 'Status' field defined in project ID $projectId (name: $projectName)."
    exit 1
}

$oldColumn = $projectColumns.options | Where-Object { $_.name -eq $oldColumnName}
if (-not $oldColumn) {
    Write-Output "Error: No state '$oldColumnName' defined in project ID $projectId (name: $projectName)."
    exit 1
}
$oldColumnId = $oldColumn.id

$newColumn = $projectColumns.options | Where-Object { $_.name -eq $newColumnName}
if (-not $newColumn) {
    Write-Output "Error: No state '$newColumnName' defined in project ID $projectId (name: $projectName)."
    exit 1
}
$newColumnId = $newColumn.id

Write-Output "Fetching all items from project: $projectId (name: $projectName)."
# Initialize variables
$hasNextPage = $true
$endCursor = $null
$allItems = @()
$pageNumber = 1
$itemsCount = 0

# Loop through pages
while ($hasNextPage) {
    Write-Output "Fetching a page #$pageNumber of items..."
    # Execute GraphQL query with pagination
    $result = gh api graphql -f query='
    query($projectId: ID!, $cursor: String) {
      node(id: $projectId) {
        ... on ProjectV2 {
          items(first: 100, after: $cursor) {
            nodes {
              id
              fieldValues(first: 10) {
                nodes{
                  ... on ProjectV2ItemFieldSingleSelectValue {
                    name
                    field {
                      ... on ProjectV2FieldCommon {
                        name
                      }
                    }
                  }
                }
              }
              content {
                ... on Issue {
                  number
                }
              }
            }
            pageInfo {
              endCursor
              hasNextPage
            }
          }
        }
      }
    }' -F projectId=$projectId -F cursor=$endCursor -F fieldId=$projectColumnId | ConvertFrom-Json

    # Append fetched items to $allItems
    $allItems += $result.data.node.items.nodes
    $pageNumber = $pageNumber + 1
    $itemsCount = $itemsCount + $allItems.Count
    # Update pagination variables
    $endCursor = $result.data.node.items.pageInfo.endCursor
    $hasNextPage = $result.data.node.items.pageInfo.hasNextPage
    Write-Output "Fetched #$itemsCount items..."
}

# Output all items
Write-Output "Fetched all items: #$itemsCount"

Write-Output "Fetching project cards for issue #$IssueId in the '$oldColumnName' column"
# Find the card associated with the issue
$issueCard = $allItems | Where-Object { $_.content.number -eq $IssueId }
if (-not $issueCard) {
    Write-Output "Error: No project card found for issue #$IssueId in project ID $projectId (name: $projectName)."
    exit 1
}

# Discover the 'Status' field value
$fieldValues = $issueCard.fieldValues.nodes
$issueCardHasStatusField = 0
$issueCardIsInOldColumnName = ""
foreach ($fieldValue in $fieldValues ) 
{
    if ($fieldValue.field.name -eq "Status") 
    {
        $issueCardHasStatusField = 1
        $issueCardIsInOldColumnName = $fieldValue.name
        break
    }
}

if ($issueCardHasStatusField -eq 0) 
{
    Write-Output "Warning: The issue #$IssueId in project ID $projectId (name: $projectName) does not have a defined 'Status' value. Proceeding without 'Status' verification."
}

if ($issueCardIsInOldColumnName -ne $oldColumnName) 
{
    if($doNotCheckOldColumnName -eq 0)
    { 
        Write-Output "Error: The issue #$IssueId in project ID $projectId (name: $projectName) cannot be moved from '$oldColumnName' to '$newColumnName' as it is in '$issueCardIsInOldColumnName'."
        exit 1
    }
    Write-Output "Moving issue #$IssueId to '$newColumnName'  in project ID $projectId (name: $projectName)."
}
else
{
    Write-Output "Moving issue #$IssueId from '$oldColumnName' to '$newColumnName'  in project ID $projectId (name: $projectName)."
}


$cardId = $issueCard.id

# Move the issue card to $newColumnName
gh api graphql -f query='
mutation($projectId: ID!, $cardId: ID!, $projectColumnId: ID!, $newColumnId: String!) {
  updateProjectV2ItemFieldValue(
    input: {
      projectId: $projectId
      itemId: $cardId
      fieldId: $projectColumnId
      value: {
        singleSelectOptionId: $newColumnId
      }
    }
  ) {
    projectV2Item {
      id
    }
  }
}' -F projectId=$projectId -F cardId=$cardId -F projectColumnId=$projectColumnId -F newColumnId=$newColumnId
