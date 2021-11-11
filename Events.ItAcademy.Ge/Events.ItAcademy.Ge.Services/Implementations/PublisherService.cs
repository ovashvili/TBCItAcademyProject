using Events.ItAcademy.Ge.Data.Repository;
using Events.ItAcademy.Ge.Domain.POCO;
using Events.ItAcademy.Ge.Services.Abstractions;
using Events.ItAcademy.Ge.Services.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.Services.Implementations
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<int> AddAsync(PublisherServiceModel director)
        {
            var item = director.Adapt<Publisher>();
            await _publisherRepository.CreateAsync(item);
            return item.PublisherID;
        }

        public async Task DeleteAsync(int id)
        {
            await _publisherRepository.DeleteAsync(id);
        }

        public Task<bool> Exists(int id)
        {
            return _publisherRepository.Exists(id);
        }

        public async Task<List<PublisherServiceModel>> GetAllAsync()
        {
            var result = await _publisherRepository.GetAllAsync();

            return result.Adapt<List<PublisherServiceModel>>();
        }

        public async Task<PublisherServiceModel> GetAsync(int id)
        {
            var result = await _publisherRepository.GetAsync(id);

            return result.Adapt<PublisherServiceModel>();
        }

        public async Task UpdateAsync(PublisherServiceModel director)
        {
            var item = director.Adapt<Publisher>();

            await _publisherRepository.UpdateAsync(item);
        }
    }
}
