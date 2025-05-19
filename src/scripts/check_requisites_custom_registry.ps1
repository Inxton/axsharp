$inxtonRegistryUrl = "https://npm.pkg.github.com/"


# Check the access to the external @inxton registry
$jsonData = Get-Content -Raw -Path "$env:USERPROFILE\.apax\auth.json" | ConvertFrom-Json


# Check if the registry exists and extract values
if ($jsonData.PSObject.Properties.Name -contains $inxtonRegistryUrl) 
{
    $registryToken = $jsonData.$inxtonRegistryUrl.registryToken
    $userName = $jsonData.$inxtonRegistryUrl.userName

    $jsonFile = $env:USERPROFILE
    $maskedPath = $jsonFile -replace '\\[^\\]+$', '\<current_user_name>\.apax\auth.json'

    if ($registryToken.Length -gt 6) 
    {
        $maskedToken = $registryToken.Substring(0,5) + ("*" * ($registryToken.Length - 6)) + $registryToken[-1]
    }else 
    {
        $maskedToken = "*" * $registryToken.Length 
    }

    if ($userName.Length -gt 2) 
    {
        $maskedUserName = $userName.Substring(0,1) + ("*" * ($userName.Length - 2)) + $userName[-1]
    }else 
    {
        $maskedUserName  = "*" * $userName.Length  
    }

    Write-Host "Registry $inxtonRegistryUrl found in $maskedPath!" -ForegroundColor Green
    Write-Host "Registry Token: $maskedToken" -ForegroundColor Green
    Write-Host "User Name: $maskedUserName" -ForegroundColor Green
    exit 0
}else 
{
    Write-Host "Registry '$inxtonRegistryUrl' not found in $maskedPath." -ForegroundColor Red

$registryGuide = @"

1. Generate a Personal Access Token on GitHub with 'read:packages' permissions (at least).
2. In AX code environment run 'apax login' command.
3. Choose the 'Custom NPM registry'
4. Enter the registry URL: $inxtonRegistryUrl 
5. Enter your username
6. Enter your personal access token
Note: Treat your personal access token like a password. Keep it secure and do not share it.
"@    
    Write-Host "You need to provide apax login to external registry." $registryGuide
    exit 1
}
