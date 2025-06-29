@echo off
REM Build script for OpenIddict Identity Service

echo Restoring NuGet packages...
dotnet restore

echo Applying Entity Framework migrations...
dotnet ef database update --project IdentityService.Web

echo Building the project...
dotnet build --no-restore --configuration Release

echo Build completed successfully.
pause
