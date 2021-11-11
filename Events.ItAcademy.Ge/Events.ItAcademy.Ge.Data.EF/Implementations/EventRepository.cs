using Events.ItAcademy.Ge.Data.EF.Abstractions;
using Events.ItAcademy.Ge.Data.Repository;
using Events.ItAcademy.Ge.Domain.POCO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.Data.EF.Implementations
{
    public class EventRepository : IEventRepository
    {
        readonly IBaseRepository<Event> _repository;
        public EventRepository(IBaseRepository<Event> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(Event _event)
        {
            await _repository.AddAsync(_event);
            return _event.EventID;
        }

        public Task DeleteAsync(int id)
        {
            return _repository.RemoveAsync(id);
        }

        public async Task<List<Event>> GetAllAsync()
        {
            //return await _repository.GetAllAsync();
            return await _repository.Table.Include(m => m.Publisher).ToListAsync();
            //return await _repository.Table.Include(m => m.Publisher).Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<Event> GetAsync(int id)
        {
            return await _repository.Table.Include(m => m.Publisher).FirstOrDefaultAsync(m => m.EventID == id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _repository.AnyAsync(x => x.EventID == id);
        }

        public Task UpdateAsync(Event _event)
        {
            return _repository.UpdateAsync(_event);
        }
    }
}
