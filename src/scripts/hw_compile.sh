export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF
hwcc=$(apax hwc compile -i ".\hwc" -o bin/hwc/)
if [[ $? -eq 0 ]]; then
	printf "${GREEN}Hardware configuration compiled succesfully.${NC}"
	exit 0
else
	printf "${RED}The compilation of the hardware configuration has finished with an error!${NC}\n"
	printf "${RED}Please check the details above.${NC}\n"
	exit 1
fi
