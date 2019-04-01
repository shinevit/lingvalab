#!/bin/bash

# write exports variable here
#1. set environment variable 'LingvaConnectionString' as connectionString

# Init Database
# Create Local Db. Do not forget add environment varaible LingvaConnectionString as system variable
dotnet ef migrations add InitialDeploy --startup-project=Lingva.WebAPI --project=Lingva.DataAccessLayer

dotnet ef database update --startup-project=Lingva.WebAPI --project=Lingva.DataAccessLayer
