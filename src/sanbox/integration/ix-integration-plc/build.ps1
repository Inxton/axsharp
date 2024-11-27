dotnet run --project ..\..\..\AXSharp.compiler\src\ixc\AXSharp.ixc.csproj --framework net7.0
$targetIP = [Environment]::GetEnvironmentVariable('AXTARGET')
$targetInput = [Environment]::GetEnvironmentVariable('AXTARGETPLATFORMINPUT')
apax build
apax sld load --accept-security-disclaimer -t $targetIP -i $targetInput -r 