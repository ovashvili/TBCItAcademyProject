using Events.ItAcademy.Ge.Data.EF.Abstractions;
using Events.ItAcademy.Ge.Data.Repository;
using Events.ItAcademy.Ge.Domain.POCO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.Data.EF.Implementations
{
    public class PublisherRepository : IPublisherRepository
    {
        readonly IBaseRepository<Publisher> _repository;
        public PublisherRepository(IBaseRepository<Publisher> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(Publisher organizer)
        {
            await _repository.AddAsync(organizer);
            return organizer.PublisherID;
        }

        public async Task<bool> Exists(int id)
        {
            return await _repository.AnyAsync(x => x.PublisherID == id);
        }

        public async Task<List<Publisher>> GetAllAsync()
        {
            //return await _repository.GetAllAsync();
            return await _repository.Table.Include(d => d.Events).ToListAsync(); ;
        }

        public async Task<Publisher> GetAsync(int id)
        {
            return await _repository.Table.Include(d => d.Events).FirstOrDefaultAsync(d => d.PublisherID == id);
        }

        public Task UpdateAsync(Publisher organizer)
        {
            return _repository.UpdateAsync(organizer);
        }

        public Task DeleteAsync(int id)
        {
            return _repository.RemoveAsync(id);
        }

    }
}
