export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF
# Function to validate IP address
validate_ip() {
    local ip=$1

    # Check if the input matches the IP address pattern
    if [[ $ip =~ ^([0-9]{1,3}\.){3}[0-9]{1,3}$ ]]; then
        # Split the IP address into its components
        IFS='.' read -r -a octets <<< "$ip"

        # Check each octet
        for octet in "${octets[@]}"; do
            if ((octet < 0 || octet > 255)); then
                return 1
            fi
        done

        # If all checks pass, it's a valid IP address
        return 0
    else
        return 1
    fi
}

# Check if the correct number of arguments are provided
if [ "$#" -ne 1 ]; then
	printf "${RED}Usage: $0 <IP_address>.${NC}"
    exit 1
fi

# Validate the input parameter
if ! validate_ip "$1"; then
	printf "${RED}The input parameter '$1' is not a valid IP address.${NC}"
    exit 1
fi
