dotnet tool restore
cd src
dotnet restore
dotnet build
dotnet test
cd CartonCapsDbContext
dotnet ef database update
cd ..
dotnet run --project ./CartonCapsAPI