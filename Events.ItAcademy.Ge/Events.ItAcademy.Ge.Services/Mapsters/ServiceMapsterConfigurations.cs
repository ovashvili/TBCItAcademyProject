using Events.ItAcademy.Ge.Domain.POCO;
using Events.ItAcademy.Ge.Services.Models;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Events.ItAcademy.Ge.Services.Mapsters
{
    public static class ServiceMapsterConfigurations
    {
        public static void AddServiceMapster(this IServiceCollection services)
        {

            TypeAdapterConfig<EventServiceModel, Event>
            .NewConfig()
            .TwoWays();

            TypeAdapterConfig<PublisherServiceModel, Publisher>
            .NewConfig()
            .Ignore(x => x.Events);

            TypeAdapterConfig<Publisher, PublisherServiceModel>
            .NewConfig()
            .Map(dest => dest.Events, src => src.Events.Select(x => new EventServiceModel { EventID = x.EventID, Name = x.Name, Date = x.Date, City = x.City, Address = x.Address }));
        }
    }
}
