using Events.ItAcademy.Ge.Data.EF.Abstractions;
using Events.ItAcademy.Ge.Data.EF.Implementations;
using Events.ItAcademy.Ge.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.ItAcademy.Ge.Data.EF.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepoInjections(this IServiceCollection services)
        {
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
