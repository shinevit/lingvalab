using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Lingva.BusinessLayer.Contracts;
using Lingva.WebAPI.Extensions;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IDictionaryService, DictionaryService>();
            services.AddScoped<ITranslaterGoogleService, TranslaterGoogleService>();
            services.AddScoped<ITranslaterYandexService, TranslaterYandexService>();
            services.AddScoped<ITranslaterService, TranslaterService>();
            services.AddScoped<ILivesearchService, LivesearchService>();
            
            // services.AddSingleton<IDinnerRepository, DinnerRepository>(); // Todo: Folow this rule for Repositories
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // loggerFactory.AddProvider(); // TODO: use Serilog

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy"); // TODO: add required
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
