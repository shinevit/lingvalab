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
using Microsoft.Extensions.Options;
using AutoMapper;
using Lingva.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Lingva.DataAccessLayer.Repositories;

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
            // получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("LingvaConnection");
            services.AddDbContext<DBContext>(options => options.UseSqlServer(connection));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<StorageOptions>(Configuration.GetSection("StorageConfig"));

            services.AddAutoMapper();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IDictionaryService, DictionaryService>();
            services.AddScoped<ITranslaterService, TranslaterService>();
            services.AddScoped<ILivesearchService, LivesearchService>();
            
            // services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://example.com"));
            });

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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
