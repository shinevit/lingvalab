using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Lingva.BusinessLayer.Contracts;
using Lingva.BusinessLayer.Interfaces;
using Lingva.WebAPI.Extensions;
using Lingva.DataAccessLayer.Repositories;
using Lingva.DataAccessLayer.Entities;
using Lingva.WebAPI.Helpers;
using Lingva.BusinessLayer.Models.Enums;

namespace Lingva.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureOptions(Configuration);
            services.ConfigureAutoMapper();
            services.ConfigureLoggerService();
            services.ConfigureUnitOfWork();
            services.ConfigureRepositories();
            services.ConfigureMVC();
            services.ConfigureDependencyInjection();
            services.ConfigureJwt(Configuration);
            
            services.AddTransient<IDictionaryService, DictionaryService>();
            services.AddTransient<ILivesearchService, LivesearchService>();
            
            services.AddTransient<TranslaterGoogleService>();
            services.AddTransient<TranslaterYandexService>();
            services.AddTransient<Func<TranslaterServices, ITranslaterService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case TranslaterServices.Yandex:
                        return serviceProvider.GetService<TranslaterYandexService>();
                    case TranslaterServices.Google:
                        return serviceProvider.GetService<TranslaterGoogleService>();
                    default:
                        return null;
                }
            });
          
            services.AddSingleton<IRepository<Word>, RepositoryWord>();
            services.AddSingleton<IRepository<DictionaryRecord>, RepositoryDictionaryRecord>();
            services.AddScoped<ISubtitlesHandler, SubtitlesHandlerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // loggerFactory.AddProvider(); // TODO: use Serilog
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy"); // TODO: add required
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
