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
using NLog.Web;
using NLog;

namespace Lingva.WebAPI
{
    public class Program
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            try
            {
                _logger.Info("Program.Main: get started.");

                var host = BuildWebHost(args);
#if DEBUG
                //_logger.Debug("Fill test data.");
                //_logger.Debug("Set initial test data.");

                //var unitOfWork = host.Services.GetService<IUnitOfWorkParser>();

                //DbInitializer.InitializeSubtitleRows(unitOfWork, false);
                //DbInitializer.InitializeParserWords(unitOfWork, false);
#endif

                host.Run();
            }
            catch(Exception e)
            {
                _logger.Error(e, e.Message);
            }
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseIISIntegration()
                //.UseUrls("http://localhost:5000")
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog()
                .Build();
        }
    }
}