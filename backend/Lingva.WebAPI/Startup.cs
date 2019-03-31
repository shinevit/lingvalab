using System;
using Lingva.BusinessLayer.Contracts;
using Lingva.BusinessLayer.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Services;
using Lingva.WebAPI.Extensions;
using Lingva.WebAPI.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Lingva.BusinessLayer.Contracts;
using Lingva.WebAPI.Extensions;
using Lingva.DataAccessLayer.Repositories;
using Lingva.DataAccessLayer.Entities;
using Lingva.WebAPI.Helpers;
using Lingva.BusinessLayer.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Lingva.WebAPI.Initializer;

namespace Lingva.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureJwt(Configuration);
            services.ConfigureSqlContext(Configuration);
            services.ConfigureOptions(Configuration);
            services.ConfigureAutoMapper();
            services.ConfigureUnitOfWork();
            services.ConfigureRepositories();
            services.ConfigureMVC();
            services.ConfigureServices();
            services.AddTransient<IDictionaryService, DictionaryService>();
            services.AddTransient<ILivesearchService, LivesearchService>();
            services.AddTransient<TranslaterGoogleService>();
            services.AddTransient<TranslaterYandexService>();
            services.AddTransient<Func<TranslaterServiceEnum, ITranslaterService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case TranslaterServiceEnum.Yandex:
                        return serviceProvider.GetService<TranslaterYandexService>();
                    case TranslaterServiceEnum.Google:
                        return serviceProvider.GetService<TranslaterGoogleService>();
                    default:
                        return null;
                }
            });

            services.AddScoped<ISubtitlesHandlerService, SubtitlesHandlerService>();
            services.AddScoped<IParserWordService, ParserWordService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlerMIddleware>();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}