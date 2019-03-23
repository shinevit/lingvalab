using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lingva.DataAccessLayer.Repositories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Lingva.WebAPI.Initializer;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
#if DEBUG
            Console.WriteLine("Fill test data.");

            var unitOfWork = host.Services.GetService<IUnitOfWorkParser>();
            DbInitializer.InitializeParserWords(unitOfWork, true);
#endif

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseIISIntegration()
                .UseUrls("http://localhost:5000")
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .Build();

    }

}
