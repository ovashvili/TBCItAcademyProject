using API.Events.ItAcademy.Ge.Models.DTO;
using Events.ItAcademy.Ge.Services.Abstractions;
using Events.ItAcademy.Ge.Services.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Events.ItAcademy.Ge.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task<IActionResult> GetAllAsycn()
        {
            var serviceResult = await _service.GetAllAsync();

            return Ok(serviceResult.Adapt<List<EventDTO>>().Where(e => e.IsActive).Where(e => !e.IsArchived).ToList());
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var serviceResult = await _service.GetAsync(id);

            if (EqualityComparer<EventServiceModel>.Default.Equals(serviceResult, default) || !serviceResult.IsActive || serviceResult.IsArchived)
                return NotFound();

            var response = serviceResult.Adapt<EventDTO>();

            return Ok(response);
        }

        // POST api/<ValuesController>
        [HttpPost]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task PostAsync([FromBody] EventDTO _event)
        {
            var model = _event.Adapt<EventServiceModel>();

            await _service.AddAsync(model);

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] EventDTO _event)
        {

            if (!await _service.Exists(id))
                return NotFound();

            var model = _event.Adapt<EventServiceModel>();

            await _service.UpdateAsync(model);

            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await _service.Exists(id))
                return NotFound();

            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}
