# run build

dotnet run --project cake/Build.csproj --do-test --do-pack --test-level 2 --framework net10.0
exit $LASTEXITCODE;