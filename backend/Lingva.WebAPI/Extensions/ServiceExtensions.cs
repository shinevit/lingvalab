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
            services.AddScoped<IUnitOfWorkDictionary, UnitOfWorkDictionary>();
            services.AddScoped<IUnitOfWorkParser, UnitOfWorkParser>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWord, RepositoryWord>();
            services.AddScoped<IRepositorySimpleEnWord, RepositorySimpleEnWord>();
            services.AddScoped<IRepositoryDictionaryEnWord, RepositoryDictionaryEnWord>();
            
            services.AddScoped<IRepositoryFilm, RepositoryFilm>();
            services.AddScoped<IRepositorySubtitle, RepositorySubtitle>();
            services.AddScoped<IRepositorySubtitleRow, RepositorySubtitleRow>();
            services.AddScoped<IRepositoryParserWord, RepositoryParserWord>();

            services.AddScoped<IRepositoryRole, RepositoryRole>();
            services.AddScoped<IRepositoryGroup, RepositoryGroup>();
            services.AddScoped<IRepositoryEvent, RepositoryEvent>();
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