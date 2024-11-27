$TARGET_IP = "192.168.0.1"
$TARGET_PLATFORM = ".\bin\1500"

# Installs dependencies
apax install

# Runs ixc compiler directly from source
dotnet run --project ..\..\..\AXSharp.compiler\src\ixc\AXSharp.ixc.csproj --framework net7.0

apax build
apax sld --accept-security-disclaimer -t $TARGET_IP -i $TARGET_PLATFORM -r --default-server-interface

cmd \c dotnet run --project ..\hello.world.console\hello.world.console.csproj --framework net7.0

apax mon -t $TARGET_IP -f monitor.mon -c
