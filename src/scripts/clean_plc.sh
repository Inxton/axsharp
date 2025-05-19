export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF

if [ "$#" -ne 3 ]; then
	printf "${RED}Usage: $0 <PLC_IP_ADDRESS> <USERNAME> <PASSWORD>.${NC}"
    exit 1
fi

PLC_IP_ADDRESS=$1
validate_script=$( dirname ${BASH_SOURCE[0]})"\\validate_ip.sh"
if ! $validate_script "$PLC_IP_ADDRESS"; then
    printf "${RED}The PLC_IP_ADDRESS '$PLC_IP_ADDRESS' is not a valid IP address.\r\n${NC}"
    exit 1
fi

USERNAME=$2
if [ -z $USERNAME ]; then
	printf "${RED}The USERNAME could not be an empty string.${NC}"
    exit 1
fi

PASSWORD=$3
if [ -z $PASSWORD ]; then
	printf "${RED}The PASSWORD could not be an empty string.${NC}"
    exit 1
fi

apax hwld --target $PLC_IP_ADDRESS --reset-plc KeepOnlyIP --username $USERNAME --password $PASSWORD --accept-security-disclaimer
if [[ $? -eq 0 ]]; then
	printf "${GREEN}PLC was reseted succesfully.${NC}"
else
	printf "${RED}Unable to reset the PLC!${NC}\n"
	printf "${RED}Please check the details above.${NC}\n"
	exit 1
fi