#!/bin/bash

# Start backend
echo "Starting backend"
dotnet run --configuration Release --project=./backend/Lingva.WebAPI/Lingva.WebAPI.csproj
