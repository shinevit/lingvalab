using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Repositories;
using Lingva.BusinessLayer;
using AutoMapper;
using Lingva.DataAccessLayer.Entities;
using System;
using Lingva.DataAccessLayer.Repositories.Lingva.DataAccessLayer.Repositories;

namespace Lingva.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            string connection = config.GetConnectionString("LingvaConnection");
            services.AddDbContext<DictionaryContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connection), ServiceLifetime.Singleton);
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
            services.AddSingleton<IUnitOfWorkDictionary, UnitOfWorkDictionary>();
            services.AddSingleton<IUnitOfWorkParser, UnitOfWorkParser>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            //services.AddSingleton<RepositoryWord>();
            //services.AddSingleton<RepositoryDictionaryRecord>();

            //services.AddScoped<IRepositoryDictionaryRecord, RepositoryDictionaryRecord>();
            services.AddScoped<IRepositoryWord, RepositoryWord>();
            services.AddScoped<IRepositorySubtitle, RepositorySubtitle>();
            services.AddScoped<IRepositorySubtitleRow, RepositorySubtitleRow>();
            services.AddScoped<IRepositoryFilm, RepositoryFilm>();
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