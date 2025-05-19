export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF
if [ -d "./.apax" ]; then
  echo "Directory "./.apax" exists!!!"
  if ! [[ -d "./gsd/source" ]]; then
    echo "Directory "./gsd/source" does not exist!!!"
    mkdir -p "./gsd/source"
  fi

    ASSETS_DIRS=$(find -L "./.apax" -type d -name 'assets')
    for DIR in $ASSETS_DIRS; do    
      sourceDirectory=$DIR
      destinationDirectory="./gsd/source"
      fileMask="[gG][sS][dD][mM][lL]*.xml"
      files=($sourceDirectory/$fileMask)
      if [ ${#files[@]} -gt 0 ] && [ "${files[0]}" != "$sourceDirectory/$fileMask" ]; then
        echo " ${#files[@]}  files is going to be copied to $destinationDirectory."
        cp $sourceDirectory/$fileMask $destinationDirectory
        echo " ${#files[@]} files copied to $destinationDirectory."
      else
          echo "No files matching the pattern '$fileMask' were found in '$sourceDirectory'."
      fi
      files=($destinationDirectory/$fileMask)
      if [ ${#files[@]} -gt 0 ] && [ "${files[0]}" != "$destinationDirectory/$fileMask" ]; then
        echo " ${#files[@]} files is going to be installed."
		hwci=$(apax hwc install-gsd --input ${destinationDirectory})
		if [[ $? -eq 1 ]]; then
			printf "${RED}The installation of the gsdml files finished with an error!${NC}\n"
			printf "${RED}Please check the details above.${NC}\n"
			exit 1
		fi
        echo " ${#files[@]} files installed"
      else
          echo "No files matching the pattern '$fileMask' were found in '$destinationDirectory'."
      fi
  done    
else
  echo "Directory "./.apax" does not exist!!!"
fi
