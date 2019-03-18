
Should use Repositories as singleton object by DI container on the Web project layer



0. Export environment variable with connection string

    Stick out this guide https://github.com/ersiner/osx-env-sync
    
    Add variable to ~/.bash_profile
    > vi ~/.bash_profile
    
    > export LingvaConnectionString="Server=192.168.1.107;Database=Lingva;Persist Security Info=False;User ID=sa;Password=0412$Antonina"
    
    > source .bash_profile
    
    Add variable to Studio
    Run:
    dotnet run --project=Lingva.WebAPI
    
2. Add Migration and deploy database for EF Core

    > dotnet ef migrations add InitialDeploy --startup-project=Lingva.WebAPI --project=Lingva.DataAccessLayer

Execute It from command line from the Solution folder.


