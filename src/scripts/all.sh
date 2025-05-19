export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF
if [ "$#" -ne 7 ]; then
    printf "${RED}Usage: $0 <NAMESPACE> <PLC_NAME> <PLC_IP_ADDRESS> <PLATFORM> <USERNAME> <PASSWORD> <USE_PLC_SIM_ADVANCED>\r\n${NC}"
    exit 1
fi

NAMESPACE=$1
if [ -z $NAMESPACE ]; then
    printf "${RED}The NAMESPACE could not be an empty string.\r\n${NC}"
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

PLATFORM=$4
if [ -z $PLATFORM ]; then
    printf "${RED}The PLATFORM could not be an empty string.\r\n${NC}"
    exit 1
fi

USERNAME=$5
if [ -z $USERNAME ]; then
    printf "${RED}The USERNAME could not be an empty string.\r\n${NC}"
    exit 1
fi

PASSWORD=$6
if [ -z $PASSWORD ]; then
    printf "${RED}The PASSWORD could not be an empty string.\r\n${NC}"
    exit 1
fi

USE_PLC_SIM_ADVANCED=$7
if [ -z $USE_PLC_SIM_ADVANCED ]; then
    printf "${RED}The USE_PLC_SIM_ADVANCED could not be an empty string.\r\n${NC}"
    exit 1
fi

PLCSIM=0
case "$(echo "$USE_PLC_SIM_ADVANCED" | tr '[:upper:]' '[:lower:]')" in
    "true")
        PLCSIM=1
        printf "${YELLOW} USE_PLC_SIM_ADVANCED is true. ${NC}"
        ;;
    "false")
        PLCSIM=0
        printf "${YELLOW} USE_PLC_SIM_ADVANCED is false. ${NC}"
        ;;
    *)
         printf "${RED}USE_PLC_SIM_ADVANCED has an invalid or undefined value: '$USE_PLC_SIM_ADVANCED'.${NC}"
        ;;
esac

certfile="./certs/$PLC_NAME/$PLC_NAME.cer" 
if ! [[ -e "$certfile" ]]; then
	printf "${RED}Certification file $certfile does not exist.\r\n${NC}"
	#alf 										#clear plc except ip and name and provide all actions for install all, build and initial download hw so as sw
	alf=$( dirname ${BASH_SOURCE[0]})"\\all_first.sh"
	$alf $NAMESPACE $PLC_NAME $PLC_IP_ADDRESS $PLATFORM $USERNAME $PASSWORD $USE_PLC_SIM_ADVANCED 
else
	check_requisites_apax_script=$( dirname ${BASH_SOURCE[0]})"\\check_requisites_apax.sh"
	if ! $check_requisites_apax_script ; then
		exit 1
	fi

	check_requisites_nuget_script=$( dirname ${BASH_SOURCE[0]})"\\check_requisites_nuget.sh"
	if ! $check_requisites_nuget_script ; then
		exit 1
	fi

	check_requisites_custom_registry_script=$( dirname ${BASH_SOURCE[0]})"\\check_requisites_custom_registry.sh"
	if ! $check_requisites_custom_registry_script ; then
		exit 1
	fi

	if [ "$PLCSIM" -eq 1 ]; then
		plcsim_script=$( dirname ${BASH_SOURCE[0]})"\\plcsimadvanced.sh"
		$plcsim_script $NAMESPACE $PLC_NAME $PLC_IP_ADDRESS
	fi

	#apax run ci                                  # clean and install dependencies
	apax clean
	apax install

	is_cert_hash_sha1_equal_script=$( dirname ${BASH_SOURCE[0]})"\\is_cert_hash_sha1_equal.sh"
	if ! $is_cert_hash_sha1_equal_script "$PLC_NAME" "$PLC_IP_ADDRESS"; then
		printf "${RED}Certification file $certfile exists, but its sha1 hash is different to the PLC's one.\r\n"
		printf "${RED}It has to be regenerated again.\r\n${NC}"
		#alf										  #clear plc except ip and name and provide all actions for install all, build and initial download hw so as sw
		alf=$( dirname ${BASH_SOURCE[0]})"\\all_first.sh"
		$alf $NAMESPACE $PLC_NAME $PLC_IP_ADDRESS $PLATFORM $USERNAME $PASSWORD $USE_PLC_SIM_ADVANCED 
	else
	
		printf "${GREEN}Certification file $certfile exists and its sha1 hash is equal to the PLC's one.\r\n"
		printf "${GREEN}No prompt will popup during execution, so you could leave your PC and enjoy your coffee now.\r\n${NC}"


		#hw_update                                    # copy and install gsd, copy templates, compile, copy the HwIds, download HW using certificate
		hw_update=$( dirname ${BASH_SOURCE[0]})"\\hw_update.sh"
		$hw_update $NAMESPACE $PLC_NAME $PLC_IP_ADDRESS $USERNAME $PASSWORD 
		if [[ $? -eq 0 ]]; then
			printf "${GREEN}Hardware configuration has been succesfully compiled and downloaded.${NC}"
		else
			printf "${RED}Compilation of the hardware configuration or its downloaded finished with an error!${NC}\n"
			printf "${RED}Please check the details above.${NC}\n"
			exit 1
		fi

		#sw_build_and_download_full                   # software build and full download
		sw_build_and_download_full=$( dirname ${BASH_SOURCE[0]})"\\sw_build_and_download_full.sh"
		$sw_build_and_download_full $PLC_NAME $PLC_IP_ADDRESS $PLATFORM $USERNAME $PASSWORD 
		if [[ $? -eq 0 ]]; then
			printf "${GREEN}Software has been succesfully compiled and downloaded.${NC}"
		else
			printf "${RED}Compilation of the software or its downloaded finished with an error!${NC}\n"
			printf "${RED}Please check the details above.${NC}\n"
			exit 1
		fi
	fi
fi 


