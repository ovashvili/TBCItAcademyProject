using Events.ItAcademy.Ge.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.Services.Abstractions
{
    public interface IEventService
    {
        Task<List<EventServiceModel>> GetAllAsync();
        Task<EventServiceModel> GetAsync(int id);
        Task<int> AddAsync(EventServiceModel _event);
        Task<bool> Exists(int id);
        Task UpdateAsync(EventServiceModel _event);
        Task DeleteAsync(int id);
    }
}
