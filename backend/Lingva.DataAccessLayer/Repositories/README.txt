
Should use Repositories as singleton object by DI container on the Web project layer



0. Export environment variable with connection string

* for macos
    Stick out this guide https://github.com/ersiner/osx-env-sync
    
    Add variable to ~/.bash_profile
    > vi ~/.bash_profile
    
    export LingvaConnectionString="Server=ipaddress;Database=Lingva;Persist Security Info=False;User ID=username;Password=password"
    
    save it (wq!)
    
    > source .bash_profile
    
    Add environment variable to Visual Studio for Mac
    
    
2. Add Migration and deploy database for EF Core

    > dotnet ef migrations add InitialDeploy --startup-project=Lingva.WebAPI --project=Lingva.DataAccessLayer

Execute It from command line from the Solution folder.


3. Run
    dotnet run --project=Lingva.WebAPI
    
    