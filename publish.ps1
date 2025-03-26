# run build

dotnet run --project cake/Build.csproj --do-test --do-pack --do-publish --test-level 100
exit $LASTEXITCODE;