using Events.ItAcademy.Ge.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.Services.Abstractions
{
    public interface IPublisherService
    {
        Task<List<PublisherServiceModel>> GetAllAsync();
        Task<PublisherServiceModel> GetAsync(int id);
        Task<int> AddAsync(PublisherServiceModel publisher);
        Task<bool> Exists(int id);
        Task UpdateAsync(PublisherServiceModel publisher);
        Task DeleteAsync(int id);
    }
}
