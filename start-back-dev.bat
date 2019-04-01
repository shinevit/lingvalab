@echo off

:: Start backend
echo "Starting backend"
dotnet run --configuration Debug --project ./backend/Lingva.WebAPI/Lingva.WebAPI.csproj
