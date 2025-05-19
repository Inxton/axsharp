export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF

if [ "$#" -ne 1 ]; then
    echo "Usage: $0 <MAC_ADDRESS>"
    exit 1
fi

MAC_ADDRESS=$1
if [ -z $MAC_ADDRESS ]; then
	printf "${RED}The MAC_ADDRESS could not be an empty string.${NC}"
    exit 1
fi

regex="^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$"

if ! [[ $MAC_ADDRESS =~ $regex ]]; then
	printf "${RED}The $MAC_ADDRESS is not valid MAC address.${NC}"
    exit 1
fi

exportdir="./dcp_export"
if ! [[ -d $exportdir ]]; then
  echo "Directory $exportdir does not exist!!!"
  mkdir -p $exportdir
fi
exportfile=$exportdir"/devices.json"
if [ -f $exportfile ]; then
  echo "File $exportfile already exist!!!"
  rm $exportfile
  echo $exportfile " was deleted!!!"
fi

apax dcp-utility discover --source-mac $MAC_ADDRESS --timeout 30000 > $exportfile