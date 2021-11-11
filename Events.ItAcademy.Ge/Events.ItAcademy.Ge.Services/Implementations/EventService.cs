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
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<int> AddAsync(EventServiceModel _event)
        {
            var item = _event.Adapt<Event>();
            await _eventRepository.CreateAsync(item);
            return item.EventID;
        }

        public async Task DeleteAsync(int id)
        {
            await _eventRepository.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _eventRepository.Exists(id);
        }

        public async Task<List<EventServiceModel>> GetAllAsync()
        {
            var result = await _eventRepository.GetAllAsync();

            return result.Adapt<List<EventServiceModel>>();
        }

        public async Task<EventServiceModel> GetAsync(int id)
        {
            var result = await _eventRepository.GetAsync(id);

            return result.Adapt<EventServiceModel>();
        }

        public async Task UpdateAsync(EventServiceModel _event)
        {
            var item = _event.Adapt<Event>();

            await _eventRepository.UpdateAsync(item);
        }
    }
}
