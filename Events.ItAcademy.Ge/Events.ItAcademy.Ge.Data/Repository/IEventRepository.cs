using Events.ItAcademy.Ge.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.Data.Repository
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<Event> GetAsync(int id);
        Task<int> CreateAsync(Event _event);
        Task<bool> Exists(int id);
        Task UpdateAsync(Event _event);
        Task DeleteAsync(int id);
    }
}
