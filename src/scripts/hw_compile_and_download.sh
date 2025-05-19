export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF
if [ "$#" -ne 5 ]; then
	printf "${RED}Usage: $0 <NAMESPACE> <PLC_NAME> <PLC_IP_ADDRESS> <USERNAME> <PASSWORD>.${NC}"
    exit 1
fi

NAMESPACE=$1
if [ -z $NAMESPACE ]; then
	printf "${RED}The NAMESPACE could not be an empty string.${NC}"
    exit 1
fi

PLC_NAME=$2
if [ -z $PLC_NAME ]; then
	printf "${RED}The PLC_NAME could not be an empty string.${NC}"
    exit 1
fi

PLC_IP_ADDRESS=$3
validate_script=$( dirname ${BASH_SOURCE[0]})"\\validate_ip.sh"
if ! $validate_script "$PLC_IP_ADDRESS"; then
	printf "${RED}The PLC_IP_ADDRESS '$PLC_IP_ADDRESS' is not a valid IP address.${NC}"
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

hw_compile_script=$( dirname ${BASH_SOURCE[0]})"\\hw_compile.sh"
if ! $hw_compile_script ; then
	exit 1
fi
hwid=$( dirname ${BASH_SOURCE[0]})"\\copy_hardware_ids.sh"
$hwid $NAMESPACE $PLC_NAME
if [[ $? -eq 0 ]]; then
	printf "${GREEN}Hardware IDs copied succesfully.${NC}"
else
	printf "${RED}Copying the hardware IDs finished with an error!${NC}\n"
	printf "${RED}Please check the details above.${NC}\n"
	exit 1
fi
hwadr=$( dirname ${BASH_SOURCE[0]})"\\copy_io_addresses.sh"
$hwadr $NAMESPACE $PLC_NAME
if [[ $? -eq 0 ]]; then
	printf "${GREEN}IO addresses copied succesfully.${NC}"
else
	printf "${RED}Copying the IO addressesfinished with an error!${NC}\n"
	printf "${RED}Please check the details above.${NC}\n"
	exit 1
fi

hw_download_only=$( dirname ${BASH_SOURCE[0]})"\\hw_download_only.sh"
$hw_download_only $PLC_NAME $PLC_IP_ADDRESS $USERNAME $PASSWORD 
if [[ $? -eq 0 ]]; then
	printf "${GREEN}Hardware configuration downloaded succesfully.${NC}"
else
	printf "${RED}Downloading hardware configuration finished with an error!${NC}\n"
	printf "${RED}Please check the details above.${NC}\n"
	exit 1
fi