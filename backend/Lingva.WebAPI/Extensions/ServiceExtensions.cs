﻿using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Repositories;
using Lingva.BusinessLayer;
using AutoMapper;
using Lingva.DataAccessLayer.Entities;
using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Lingva.BusinessLayer.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lingva.BusinessLayer.Services;
using Lingva.DataAccessLayer.Repositories.Lingva.DataAccessLayer.Repositories;
using Lingva.WebAPI.Helpers;
using Swashbuckle.AspNetCore.Swagger;
using Lingva.DataAccessLayer.InitializeWithTestData;
using System.Reflection;
using System.IO;

namespace Lingva.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            string configStringValue = config.GetConnectionString("LingvaConnection");
            string configVariableName = configStringValue.GetVariableName();
            string connectionStringValue = Environment.GetEnvironmentVariable(configVariableName);

            services.AddDbContext<DictionaryContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connectionStringValue));
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFilmService, FilmService>();
        }

        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Lingvalab",
                    Version = "v1",
                    Description = "Demo app of Dp-155 .NET"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
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
            services.AddScoped<IUnitOfWorkUser, UnitOfWorkUser>();
            services.AddScoped<IUnitOfWorkFilm, UnitOfWorkFilm>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(EfRepository<>));

            services.AddScoped<IRepositoryWord, RepositoryWord>();
            services.AddScoped<IRepositoryDictionaryRecord, RepositoryDictionaryRecord>();
            services.AddScoped<IRepositoryUser, RepositoryUser>();

            services.AddScoped<IRepositoryWord, RepositoryWord>();
            services.AddScoped<IRepositorySimpleEnWord, RepositorySimpleEnWord>();
            services.AddScoped<IRepositoryDictionaryEnWord, RepositoryDictionaryEnWord>();

            services.AddScoped<IRepositoryFilm, RepositoryFilm>();
            services.AddScoped<IRepositorySubtitle, RepositorySubtitle>();
            services.AddScoped<IRepositorySubtitleRow, RepositorySubtitleRow>();
            services.AddScoped<IRepositoryParserWord, RepositoryParserWord>();
            services.AddScoped<IRepositoryLanguage, RepositoryLanguage>();

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

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {                           
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };                
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });
        }

        public static void ConfigureMVC(this IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
    }
}