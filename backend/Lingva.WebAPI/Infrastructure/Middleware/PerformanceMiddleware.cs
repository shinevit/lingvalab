using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Infrastructure.Middleware
{
    

    // To calculate duration of requests
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;

        public PerformanceMiddleware(RequestDelegate next) // , ILoggerManager logger
        {
            // _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            // httpContext.Request.Body.
            // log request

            var perfCounter = Stopwatch.StartNew();

            await _next(httpContext);

            perfCounter.Stop();

            // log response
            long elapsedms = perfCounter.ElapsedMilliseconds;

            httpContext.Response.Headers.Add("ElapsedTime", elapsedms.ToString());
        }
    }
}
