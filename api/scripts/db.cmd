@ECHO OFF
@del /s /q .\api\src\Migrations
@dotnet ef database drop --project api/src --force --no-build
@dotnet build api/src
@dotnet ef migrations add Initial --project api/src --no-build
@dotnet build api/src
@dotnet ef database update --project api/src --no-build
