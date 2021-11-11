using API.Events.ItAcademy.Ge.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Events.ItAcademy.Ge.Infrastructure.Extensions
{
    public static class LoggerMiddlewareExtension
    {
        internal static IApplicationBuilder UseLoggerMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<RequestResponseLoggerMiddleware>();

            return builder;
        }
    }
}
