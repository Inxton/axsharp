export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF

if [ "$#" -ne 4 ]; then
	printf "${RED}Usage: $0 <PLC_IP_ADDRESS> <PLC_NAME> <USERNAME> <PASSWORD>.${NC}"
    exit 1
fi
PLC_IP_ADDRESS=$1
validate_script=$( dirname ${BASH_SOURCE[0]})"\\validate_ip.sh"
if ! $validate_script "$PLC_IP_ADDRESS"; then
	printf "${RED}The PLC_IP_ADDRESS '$PLC_IP_ADDRESS' is not a valid IP address.${NC}"
    exit 1
fi

PLC_NAME=$2
if [ -z "$PLC_NAME" ]; then
	printf "${RED}The PLC_NAME could not be an empty string.${NC}"
    exit 1
fi

USERNAME=$3
if [ -z $USERNAME ]; then
	printf "${RED}The USERNAME could not be an empty string.${NC}"
    exit 1
fi

PASSWORD=$4
if [ -z $PASSWORD ]; then
	printf "${RED}The PASSWORD could not be an empty string.${NC}"
    exit 1
fi

PLC_CERT=./certs/$PLC_NAME/$PLC_NAME.cer
if ! [[ -e $PLC_CERT ]]; then
	printf "${RED}Certificate file $PLC_CERT not found!!!${NC}"
    exit 1
fi
apax hw-diag list --target $PLC_IP_ADDRESS --username $USERNAME --password $PASSWORD --certificate $PLC_CERT
