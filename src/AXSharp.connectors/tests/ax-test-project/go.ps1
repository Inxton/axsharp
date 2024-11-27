$targetIP = [Environment]::GetEnvironmentVariable('AX_WEBAPI_TARGET')
$targetInput = [Environment]::GetEnvironmentVariable('AXTARGETPLATFORMINPUT')
$targetIP
$targetInput
apax install
apax build
#apax sld --accept-security-disclaimer -t $targetIP -i $targetInput -r --default-server-interface
apax sld load -t $targetIP -i $targetInput -r --accept-security-disclaimer