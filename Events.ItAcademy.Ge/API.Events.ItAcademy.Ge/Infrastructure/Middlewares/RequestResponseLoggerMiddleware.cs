using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Events.ItAcademy.Ge.Infrastructure.Middlewares
{
    public class RequestResponseLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggerMiddleware> _logger;
        public RequestResponseLoggerMiddleware(RequestDelegate next, ILogger<RequestResponseLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"***Request Details***{Environment.NewLine}Content Types: {context.Request.ContentType}{Environment.NewLine}Scheme: {context.Request.Scheme}{Environment.NewLine}");

            await _next(context);

            _logger.LogInformation($"***Response Details***{Environment.NewLine}Content Types: {context.Response.ContentType}{Environment.NewLine}Status Code: {context.Response.StatusCode}{Environment.NewLine}");
        }
    }
}
