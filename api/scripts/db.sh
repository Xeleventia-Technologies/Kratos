# dotnet ef migrations remove --project src &&
dotnet ef database drop --project src --force && 
dotnet ef migrations add Initial  --project src && 
dotnet ef database update --project src
# rm -rf ./src/Migrations/*
