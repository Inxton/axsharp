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

#copy_and_install_gsd                         # copy and install all gsdml files from library           
copy_and_install_gsd=$( dirname ${BASH_SOURCE[0]})"\\copy_and_install_gsd.sh"
$copy_and_install_gsd
if [[ $? -eq 0 ]]; then
	printf "${GREEN}Gsdml files installed succesfully.${NC}"
else
	printf "${RED}Installation of the gsdml files finished with an error!${NC}\n"
	printf "${RED}Please check the details above.${NC}\n"
	exit 1
fi

#copy_hwl_templates                           # copy all templates from library  
copy_hwl_templates=$( dirname ${BASH_SOURCE[0]})"\\copy_hwl_templates.sh"
$copy_hwl_templates
if [[ $? -eq 0 ]]; then
	printf "${GREEN}Copying hardware templates from the libraries finished succesfully.${NC}"
else
	printf "${RED}Copying hardware templates from the libraries finished with an error!${NC}\n"
	printf "${RED}Please check the details above.${NC}\n"
	exit 1
fi

#hw_first_compile_and_first_download          # compile, copy the HwIds, first download HW using password and upload certificate       
hw_first_compile_and_first_download=$( dirname ${BASH_SOURCE[0]})"\\hw_first_compile_and_first_download.sh"
$hw_first_compile_and_first_download $NAMESPACE $PLC_NAME $PLC_IP_ADDRESS $USERNAME $PASSWORD 
if [[ $? -eq 0 ]]; then
	printf "${GREEN}Hardware configuration has been succesfully compiled and downloaded.${NC}"
else
	printf "${RED}Compilation of the hardware configuration or its downloaded finished with an error!${NC}\n"
	printf "${RED}Please check the details above.${NC}\n"
	exit 1
fi
