export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF

# Function to validate IP address with CIDR notation
validate_ip_cidr() {
    local ip_cidr=$1

    # Check if the input matches the IP address with CIDR notation pattern
    if [[ $ip_cidr =~ ^([0-9]{1,3}\.){3}[0-9]{1,3}\/([0-9]|[12][0-9]|3[0-2])$ ]]; then
        # Extract the IP address part
        local ip=${ip_cidr%/*}
        
        # Split the IP address into its components
        IFS='.' read -r -a octets <<< "$ip"

        # Check each octet
        for octet in "${octets[@]}"; do
            if ((octet < 0 || octet > 255)); then
                return 1
            fi
        done

        # If all checks pass, it's a valid IP address with CIDR notation
        return 0
    else
        return 1
    fi
}

# Check if the correct number of arguments are provided
if [ "$#" -ne 1 ]; then
    echo "Usage: $0 <IP_address/CIDR>"
    exit 1
fi

# Validate the input parameter
# if validate_ip_cidr "$1"; then
    # echo "The input parameter '$1' is a valid IP address with CIDR notation."
# else
if ! validate_ip_cidr "$1"; then
    echo "The input parameter '$1' is not a valid IP address with CIDR notation."
    exit 1
fi
