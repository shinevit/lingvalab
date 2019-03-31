#!/bin/bash

dotnet restore
dotnet build

# Create Local Db. Do not forget add environment varaible LingvaConnectionString to User variable
dotnet ef migrations add InitialDeploy --startup-project=Lingva.WebAPI --project=Lingva.DataAccessLayer

dotnet ef database update --startup-project=Lingva.WebAPI --project=Lingva.DataAccessLayer
