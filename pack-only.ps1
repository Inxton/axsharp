# run build

dotnet run --project cake/Build.csproj --do-pack --test-level 1
exit $LASTEXITCODE;