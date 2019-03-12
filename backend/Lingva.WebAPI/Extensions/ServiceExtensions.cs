using Microsoft.Extensions.Configuration;
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
using Lingva.WebAPI.Helpers;
using Lingva.BusinessLayer.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lingva.BusinessLayer.Services;

namespace Lingva.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            string connection = config.GetConnectionString("LingvaConnection");
            services.AddDbContext<DictionaryContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connection));
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
            services.AddScoped<IUnitOfWorkUser, UnitOfWorkUser>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWord, RepositoryWord>();
            services.AddScoped<IRepositoryDictionaryRecord, RepositoryDictionaryRecord>();
            services.AddScoped<IRepositoryUser, RepositoryUser>();           
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
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