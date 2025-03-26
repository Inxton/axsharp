# run build

dotnet run --project cake/Build.csproj --do-pack --test-level 1 --framework net9.0
exit $LASTEXITCODE;