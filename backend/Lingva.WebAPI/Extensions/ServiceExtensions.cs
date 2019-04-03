﻿using Microsoft.EntityFrameworkCore;
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
using Microsoft.IdentityModel.Protocols;
using Lingva.WebAPI.Helpers;
using Lingva.WebAPI.Initializer;
using Swashbuckle.AspNetCore.Swagger;
using Lingva.DataAccessLayer.InitializeWithTestData;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Lingva.DataAccessLayer.Repositories.Lingva.DataAccessLayer.Repositories;

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

            services.AddTransient<DictionaryContext>();
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
                c.AddSecurityDefinition("Bearer", 
                new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Bearer", Enumerable.Empty<string>() },
                });
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
            services.AddScoped<IUnitOfWorkGroup, UnitOfWorkGroup>();
            services.AddScoped<IUnitOfWorkUserGroup, UnitOfWorkUserGroup>();
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
            services.AddScoped<IRepositoryGroup, RepositoryGroup>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IRepositoryEvent, RepositoryEvent>();
            services.AddScoped<IRepositoryUserGroup, RepositoryUserGroup>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddTransient<IFilmService, FilmService>();
            services.AddTransient<IGroupService, GroupService>();
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