using Lingva.WebAPI.Dto;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Infrastructure.Middleware
{
    public class ExceptionHandlerMIddleware
    {
        private static  readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly RequestDelegate _next;
        // private readonly ILoggerManager _logger;

        public ExceptionHandlerMIddleware(RequestDelegate next) // , ILoggerManager logger
        {
            // _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;

            var responseDto = new BaseStatusDto();

            responseDto.AddResponseInfo(context.Response.StatusCode, exception.Message);

            return context.Response.WriteAsync(JsonConvert.SerializeObject(responseDto, serializerSettings)); // 
        }
    }
}