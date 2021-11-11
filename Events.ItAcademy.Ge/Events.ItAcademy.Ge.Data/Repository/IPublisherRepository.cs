using Events.ItAcademy.Ge.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.Data.Repository
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher> GetAsync(int id);
        Task<int> CreateAsync(Publisher publisher);
        Task<bool> Exists(int id);
        Task UpdateAsync(Publisher publisher);
        Task DeleteAsync(int id);
    }
}
