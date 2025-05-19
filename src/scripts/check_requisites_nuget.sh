feedUrl="https://nuget.pkg.github.com/inxton/index.json"

export GREEN='\033[0;32m'
export RED='\033[0;31m'
export YELLOW='\033[0;33m'
export NC='\033[0m\r\n' # No Color+CRLF

# Set headers
#userToken="your_token_here"  # Replace with actual user token
headers=(
    -H "Authorization: Bearer $userToken"
    -H "User-Agent: Bash"
    -H "Accept: application/vnd.github.package-preview+json"
)


# Check if the feed is added
is_feed_already_added(){
	feeds=$(dotnet nuget list source)

	if echo "$feeds" | grep -q "$feedUrl"; then
		printf "${GREEN}The NuGet feed with URL $feedUrl is already added.${NC}"
		return 0
	else
		printf "${RED}The NuGet feed with URL $feedUrl is not added.${NC}"
		printf "${RED}You will need to add $feedUrl to your nuget sources manually (more information in src/README.md).${NC}"
		return 1 
	fi
}
# Check if the feed is accessible by means of network
has_feed_access(){
    response=$(curl -s "${headers[@]}" -w "%{http_code}" -o /dev/null "$feedUrl")
    if [[ "$response" -eq 200 ]]; then
        printf "${GREEN}Feed: $feedUrl accessible by means of network.${NC}"
        return 0 
    else
        printf "${RED}Failed to access feed: $feedUrl. Error: HTTP status $response.${NC}"
        printf "${RED}Try to access it manually, check your connection, firewall settings, etc.${NC}"
		return 1
    fi
}

# Check if the feed is authorized
has_feed_authorization_passed(){
    # if dotnet tool update axsharp.ixc --prerelease; then
        # printf "${GREEN}Authentication passed successfully while accessing feed $feedUrl.${NC}"
        # return 0 
    # else
        # printf "${RED}Authentication failed while accessing feed $feedUrl.${NC}"
		# return 1
    # fi
	return 0 
}

# Check if the correct number of arguments are provided
if [ "$#" -ne 0 ]; then
	printf "${RED}Invalid number of parameters.${NC}"
	printf "${RED}Usage: $0 ${NC}"
	exit 1
fi


# If any of the checks failed, provide manual instructions
if ! is_feed_already_added || ! has_feed_access || ! has_feed_authorization_passed ; then
    nugetGuide=$(cat <<EOF
To manually add the GitHub NuGet feed to your sources:

1. Generate a Personal Access Token on GitHub with 'read:packages', 'write:packages', and 'delete:packages' (if needed) permissions.
2. Open a command prompt or terminal.
3. Use the following command to add the feed to your NuGet sources:
   nuget sources Add -Name "GitHub" -Source "$feedUrl" -Username [YOUR_GITHUB_USERNAME] -Password [YOUR_PERSONAL_ACCESS_TOKEN]

Replace [YOUR_GITHUB_USERNAME] with your actual GitHub username and [YOUR_PERSONAL_ACCESS_TOKEN] with the token you generated.

Note: Treat your personal access token like a password. Keep it secure and do not share it.
EOF
)
    printf "${RED}You need to add the GitHub NuGet feed to your sources manually.${NC}"
    echo "$nugetGuide"
    exit 1
else
    exit 0
fi
