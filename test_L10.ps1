# run build

dotnet run --project cake/Build.csproj --do-test --test-level 10 --framework net10.0
exit $LASTEXITCODE;