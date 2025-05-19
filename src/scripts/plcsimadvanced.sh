export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF

if [ "$#" -ne 3 ]; then
    printf "${RED}Usage: $0 <INSTANCE_NAME> <PLC_NAME> <PLC_IP_ADDRESS>\r\n${NC}"
    exit 1
fi

INSTANCE_NAME=$1
if [ -z $INSTANCE_NAME ]; then
    printf "${RED}The INSTANCE_NAME could not be an empty string.\r\n${NC}"
    exit 1
fi

PLC_NAME=$2
if [ -z $PLC_NAME ]; then
    printf "${RED}The PLC_NAME could not be an empty string.\r\n${NC}"
    exit 1
fi

PLC_IP_ADDRESS=$3
validate_script=$( dirname ${BASH_SOURCE[0]})"\\validate_ip.sh"
if ! $validate_script "$PLC_IP_ADDRESS"; then
    printf "${RED}The PLC_IP_ADDRESS '$PLC_IP_ADDRESS' is not a valid IP address.\r\n${NC}"
    exit 1
fi

dotnet run --project ..\\tools\\AXOpen.Simple.Tools\\AXOpen.Simple.Tools\\AXOpen.Simple.Tools.csproj -- startplcsim -x $INSTANCE_NAME -n $PLC_NAME -t $PLC_IP_ADDRESS
