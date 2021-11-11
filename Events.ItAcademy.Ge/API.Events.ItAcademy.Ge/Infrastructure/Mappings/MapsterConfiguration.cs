using API.Events.ItAcademy.Ge.Models.DTO;
using Events.ItAcademy.Ge.Services.Mapsters;
using Events.ItAcademy.Ge.Services.Models;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Events.ItAcademy.Ge.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<EventDTO, EventServiceModel>
            .NewConfig()
            .TwoWays();

            TypeAdapterConfig<PublisherDTO, PublisherServiceModel>
            .NewConfig()
            .Ignore(x => x.Events);

            TypeAdapterConfig<PublisherServiceModel, PublisherDTO>
            .NewConfig()
            .Map(dest => dest.Events, src => src.Events.Select(x => new EventDTO { EventID = x.EventID, Name = x.Name, Date = x.Date, City = x.City, Address = x.Address }));

            services.AddServiceMapster();
        }
    }
}
