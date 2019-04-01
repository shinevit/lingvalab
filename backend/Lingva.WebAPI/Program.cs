using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Lingva.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseIISIntegration()
                .UseUrls("http://localhost:5000")
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .Build();
        }
    }
}