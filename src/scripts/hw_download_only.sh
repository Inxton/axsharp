export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF
if [ "$#" -ne 4 ]; then
	printf "${RED}Usage: $0 <PLC_NAME> <PLC_IP_ADDRESS> <USERNAME> <PASSWORD>.${NC}"
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

USERNAME=$3
if [ -z $USERNAME ]; then
    printf "${RED}The USERNAME could not be an empty string.\r\n${NC}"
    exit 1
fi

PASSWORD=$4
if [ -z $PASSWORD ]; then
    printf "${RED}The PASSWORD could not be an empty string.\r\n${NC}"
    exit 1
fi

if ! [[ -d "./hwc" ]]; then
	printf "${RED}Directory ".\hwc" does not exist!!!${NC}"
	exit 1
fi
hwcfile=".\hwc\\${PLC_NAME}.hwl.yml"
if ! [[ -e "$hwcfile" ]]; then
	printf "${RED}Hardware configuration file $hwcfile does not exist!!!${NC}"
	exit 1
fi 

certfile="./certs/$PLC_NAME/$PLC_NAME.cer"
if ! [[ -e "$certfile" ]]; then
	printf "${RED}Certification file $certfile does not exist!!!${NC}"
	exit 1
fi        

apax hwld --input bin/hwc/$PLC_NAME --target $PLC_IP_ADDRESS  --username $USERNAME --password $PASSWORD --certificate $certfile --no-input --accept-security-disclaimer --log Information
if [[ $? -eq 0 ]]; then
	printf "${GREEN}Hardware configuration has been succesfully downloaded.${NC}"
else
	printf "${RED}Downloading of the hardware configuration finished with an error!${NC}\n"
	printf "${RED}Please check the details above.${NC}\n"
	exit 1
fi

