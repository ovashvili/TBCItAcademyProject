using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.CMVC.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection services)
        {
            //TypeAdapterConfig<EventDTO, EventServiceModel>
            //.NewConfig()
            //.TwoWays();

            //TypeAdapterConfig<OrganizerDTO, OrganizerServiceModel>
            //.NewConfig()
            //.Ignore(x => x.Events);

            //TypeAdapterConfig<OrganizerServiceModel, OrganizerDTO>
            //.NewConfig()
            //.Map(dest => dest.Events, src => src.Events.Select(x => new EventDTO { EventID = x.EventID, Name = x.Name, Date = x.Date, City = x.City, Address = x.Address }));

            //services.AddServiceMapster();
        }
    }
}
