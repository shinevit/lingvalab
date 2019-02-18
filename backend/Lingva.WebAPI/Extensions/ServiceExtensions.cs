using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Repositories;
using Lingva.BusinessLayer;
using AutoMapper;

namespace Lingva.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            string connection = config.GetConnectionString("LingvaConnection");
            services.AddDbContext<DBContext>(options => options.UseSqlServer(connection));
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            //services.AddSingleton<ILoggerFactory, LoggerManager>();
        }

        public static void ConfigureOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<StorageOptions>(config.GetSection("StorageConfig"));
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();
        }        
    }
}