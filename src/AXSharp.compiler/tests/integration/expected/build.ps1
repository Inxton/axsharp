cd lib1
dotnet run --project ..\..\..\src\ixc\AXSharp.ixc.csproj --framework net7.0
dotnet build ./ix/lib1.csproj
cd ..

cd lib2
dotnet run --project ..\..\..\src\ixc\AXSharp.ixc.csproj --framework net7.0
dotnet build ./ix/lib2.csproj
cd ..

cd app
dotnet run --project ..\..\..\..\src\ixc\AXSharp.ixc.csproj --framework net7.0
dotnet build ./ix/app.csproj
cd ..