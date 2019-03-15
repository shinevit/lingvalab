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
using Lingva.DataAccessLayer.Repositories;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.InitializeWithTestData;

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
            services.ConfigureSqlContext(Configuration);
            services.ConfigureOptions(Configuration);
            services.ConfigureAutoMapper();
            services.ConfigureUnitOfWork();
            services.ConfigureRepositories();
            services.AddScoped(typeof(IGenericRepository<>), typeof(EfRepository<>));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IWordService, WordService>();
            services.AddTransient<IDictionaryService, DictionaryService>();
            services.AddTransient<ILivesearchService, LivesearchService>();
            services.AddTransient<ISubtitlesHandlerService, SubtitlesHandlerService>();
            services.AddTransient<TranslaterGoogleService>();
            services.AddTransient<TranslaterYandexService>();
            services.AddTransient<Func<string, ITranslaterService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "g":
                        return serviceProvider.GetService<TranslaterGoogleService>();
                    case "y":
                        return serviceProvider.GetService<TranslaterYandexService>();
                    default:
                        return null;
                }
            });

            services.AddSingleton<IRepository<Word>, RepositoryWord>();
            services.AddSingleton<IRepository<DictionaryRecord>, RepositoryDictionaryRecord>();
            services.AddScoped<ISubtitlesHandlerService, SubtitlesHandlerService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy"); // TODO: add required
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();

            DbInitializer.InitializeParserWords(app);
        }
       
    }
}
