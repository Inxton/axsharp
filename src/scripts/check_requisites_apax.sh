apaxUrl="https://console.simatic-ax.siemens.io/"
expectedApaxVersion="4.0.0"

export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF

# Function to check if the apax is installed 
is_apax_installed(){
	# Try to get the apax version
	apaxVersion=$(apax --version 2>/dev/null)

	if [[ $? -eq 0 ]]; then
		printf "${GREEN}Apax installed.${NC}"
		return 0
	else
		printf "${RED}Apax is not installed or not found in PATH. You need to have a valid SIMATIC-AX license.${NC}"
		return 1
	fi
}

# Function to check if the if the apax version matches
is_apax_version_equal(){
	local expected=$1
	# Get the apax version
	apaxVersion=$(apax --version 2>/dev/null)

	if [[ "$apaxVersion" == "$expected" ]]; then
		printf "${GREEN}Apax $expected detected.${NC}"
		return 0
	else
		printf "${RED}Apax version mismatch. Expected $expected but found $apaxVersion.${NC}"
		printf "${RED}Run apax self-update $expected.${NC}"
		return 1
	fi
}

# Function to check if the if the apax site is accessible
is_apax_site_accessible(){
    # Just check the access by trying to get the feed
    response=$(curl -L -s -o /dev/null -w "%{http_code}" "$apaxUrl")

    if [[ "$response" -eq 200 ]]; then
        printf "${GREEN}Feed: $apaxUrl accessible by means of network.${NC}"
        return 0
    else
        printf "${RED}Failed to access feed: $apaxUrl. Error: HTTP status $response.${NC}"
        printf "${RED}Try to access it manually, check your connection, firewall settings, credentials, etc.${NC}"
		return 1
    fi
}

# Function to check for valid access to the apax registries
has_access_to_apax_registries(){
    command="apax info --ax-scopes"
    output=$(eval "$command 2>&1")
    if echo "$output" | grep -q "No access to the Simatic-AX registry"; then
        printf "${RED}Unable to access apax registries. Check your connections, firewall, credentials etc.${NC}"
        printf "${RED}$output.${NC}"
		return 1
    else
        printf  "${GREEN}Apax registries are accessible.${NC}"
        return 0
    fi
}

# Check if the correct number of arguments are provided
if [ "$#" -ne 0 ]; then
	printf "${RED}Invalid number of parameters.${NC}"
	printf "${RED}Usage: $0 ${NC}"
	exit 1
fi

if ! is_apax_installed ; then
    printf "${RED}Apax is not installed or not found in PATH. You need to have a valid SIMATIC-AX license.${NC}"
    exit 1
elif ! is_apax_version_equal "$expectedApaxVersion" ; then
    printf "${RED}Apax installed, but version found does not match the verion required $expectedApaxVersion.${NC}"
    exit 1
else
    printf "${GREEN}Apax installed, verion matches required $expectedApaxVersion.${NC}"
	if ! is_apax_site_accessible ; then
    	printf "${RED}Failed to access feed: $apaxUrl. Error: HTTP status $response.${NC}"
        printf "${RED}Try to access it manually, check your connection, firewall settings, credentials, etc.${NC}"
    	exit 1
	elif ! has_access_to_apax_registries ; then
		printf "${RED}Feed: $apaxUrl accessible by means of network,${NC}"
		printf "${RED}but there is no access to the apax registries.${NC}"
		printf "${RED}Check your connection, firewall settings, credentials, etc.${NC}"
		exit 1
	else	
	    printf  "${GREEN}Apax registries accessible, credentials verified.${NC}"
		exit 0	
    fi
fi
