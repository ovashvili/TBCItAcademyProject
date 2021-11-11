using Events.ItAcademy.Ge.Data.EF.Extensions;
using Events.ItAcademy.Ge.Services.Abstractions;
using Events.ItAcademy.Ge.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.ItAcademy.Ge.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPublisherService, PublisherService>();
            services.AddTransient<IEventService, EventService>();
            services.AddRepoInjections();
        }
    }
}
