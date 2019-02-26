﻿using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Repositories;
using Lingva.BusinessLayer;
using AutoMapper;
using Lingva.DataAccessLayer.Entities;
using System;

namespace Lingva.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            string connection = config.GetConnectionString("LingvaConnection");
            services.AddDbContext<DictionaryContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);
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
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IRepository<Word>, RepositoryWord>();
            services.AddSingleton<IRepository<DictionaryRecord>, RepositoryDictionaryRecord>();

            //services.AddSingleton<RepositoryWord>();
            //services.AddSingleton<RepositoryDictionaryRecord>();

            //services.AddSingleton<Func<Type, IRepository<object>>>(serviceProvider => key =>
            //{
            //    return (IRepository<object>)serviceProvider.GetService<IRepository<Type>>();
            //});
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