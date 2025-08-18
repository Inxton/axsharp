export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF

if [ "$#" -ne 5 ]; then
	printf "${RED}Usage: $0 <PLC_NAME> <PLC_IP_ADDRESS> <PLATFORM> <USERNAME> <PASSWORD>.${NC}"
    exit 1
fi

PLC_NAME=$1
if [ -z $PLC_NAME ]; then
	printf "${RED}The PLC_NAME could not be an empty string.${NC}"
    exit 1
fi

PLC_IP_ADDRESS=$2
validate_script=$( dirname ${BASH_SOURCE[0]})"\\validate_ip.sh"
if ! $validate_script "$PLC_IP_ADDRESS"; then
	printf "${RED}The PLC_IP_ADDRESS '$PLC_IP_ADDRESS' is not a valid IP address.${NC}"
    exit 1
fi

PLATFORM=$3
if [ -z $PLATFORM ]; then
	printf "${RED}The PLATFORM could not be an empty string.${NC}"
    exit 1
fi

USERNAME=$4
if [ -z $USERNAME ]; then
    printf "${RED}The USERNAME could not be an empty string.\r\n${NC}"
    exit 1
fi

PASSWORD=$5
if [ -z $PASSWORD ]; then
    printf "${RED}The PASSWORD could not be an empty string.\r\n${NC}"
    exit 1
fi

apax build
dotnet ixc

#sw_download_delta
sw_download_delta=$( dirname ${BASH_SOURCE[0]})"\\sw_download_delta.sh"
$sw_download_delta $PLC_NAME $PLC_IP_ADDRESS $PLATFORM $USERNAME $PASSWORD 
if [[ $? -eq 0 ]]; then
	printf "${GREEN}Software changes has been succesfully downloaded using security certificate.${NC}"
else
	printf "${RED}Downloading of the software changes using security certificate finished with an error!${NC}\n"
	printf "${RED}Please check the details above.${NC}\n"
	exit 1
fi