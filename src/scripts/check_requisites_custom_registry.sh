scriptDir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF

powershellScript="$scriptDir/check_requisites_custom_registry.ps1"

powershell.exe -File "$powershellScript"
exitCode=$?  

# Evaluate the exit code
if [[ $exitCode -eq 0 ]]; then
    printf "${GREEN}Custom registry found."
	exit 0
else
    printf "${RED}Custom registry not found."
    exit 1  
fi