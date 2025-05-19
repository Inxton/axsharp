export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF

if [ "$#" -ne 2 ]; then
	printf "${RED}Usage: $0 <NAMESPACE> <PLC_NAME>"
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
if ! [[ -d "./hwc" ]]; then
	printf "${RED}Directory ".\hwc" does not exist!!!${NC}"
	exit 1
fi
input_file=SystemConstants/$PLC_NAME"_HwIdentifiers.st"
if ! [[ -e $input_file ]]; then
	printf "${RED}File $input_file does not exist!!!${NC}"
	exit 1
fi
output_dir=src/IO
if ! [[ -d $output_dir ]]; then
  echo "Directory $output_dir does not exist!!!"
  mkdir -p $output_dir
fi
output_file="$output_dir/HwIdentifiers.st"
lines_to_replace=("CONFIGURATION HardwareIDs" "VAR_GLOBAL CONSTANT" "END_VAR" "END_CONFIGURATION")
old_substrings=(": UINT := UINT" ";")
new_substrings=(":=	WORD" ",")
echo "NAMESPACE ${NAMESPACE}" > "$output_file"
echo "    TYPE" >> "$output_file"
echo "        HwIdentifiers : WORD" >> "$output_file"
echo "        (" >> "$output_file"
while IFS= read -r line; do
  copy_this_line=true
  for line_to_replace in "${lines_to_replace[@]}"; do
    if grep -qF "$line_to_replace" <<< "$line"; then
      copy_this_line=false
      break
    fi
  done
  if $copy_this_line; then
    modified_line="$line"
    for ((i=0; i<${#old_substrings[@]}; i++)); do
      old_substring="${old_substrings[i]}"
      new_substring="${new_substrings[i]}"
      modified_line="${modified_line//$old_substring/$new_substring}"
	  # Check if the variable starts with a letter
	  # Find the position of the first non-whitespace character
	  first_non_white_pos=$(expr match "$modified_line" '^[[:space:]]*')
	  
	  # Extract the first non-whitespace character
	  first_char="${modified_line:$first_non_white_pos:1}"
	  
	  # Check if the string contains only whitespace
	  if [[ "$first_non_white_pos" -lt "${#modified_line}" ]]; then
	  	# Check if the first non-whitespace character is not a letter
	  	if [[ ! "$first_char" =~ [a-zA-Z] ]]; then
	  		# Split the string into two parts: before and after the first non-whitespace character
	  		before_first_char="${modified_line:0:$first_non_white_pos}"
	  		after_first_char="${modified_line:$first_non_white_pos}"
	  		
	  		# Avoid adding an extra underscore if the first character is already an underscore
	  		if [[ "$first_char" != "_" ]]; then
	  			modified_line="${before_first_char}_${after_first_char}"
	  		fi
	  	fi
	  fi
    done
    echo "    $modified_line" >> "$output_file"
  fi
done < "$input_file"
echo "            NONE := WORD#0" >> "$output_file"
echo "        );" >> "$output_file"
echo "    END_TYPE" >> "$output_file"
echo "END_NAMESPACE" >> "$output_file"