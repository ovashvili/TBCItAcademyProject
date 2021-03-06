using API.Events.ItAcademy.Ge.Models.DTO;
using Events.ItAcademy.Ge.Services.Abstractions;
using Events.ItAcademy.Ge.Services.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Events.ItAcademy.Ge.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AdminEventController : ControllerBase
    {
        private readonly IEventService _service;

        public AdminEventController(IEventService service)
        {
            _service = service;
        }

        [HttpGet]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task<IActionResult> GetAllAsycn()
        {
            var serviceResult = await _service.GetAllAsync();

            return Ok(serviceResult.Adapt<List<EventDTO>>().Where(e => e.IsActive == false).ToList());
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var serviceResult = await _service.GetAsync(id);

            if (EqualityComparer<EventServiceModel>.Default.Equals(serviceResult, default) && serviceResult.IsActive)
                return NotFound();

            var response = serviceResult.Adapt<EventDTO>();

            return Ok(response);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task<IActionResult> PublishAsync(int id, [FromBody] EventDTO _event)
        {

            if (!await _service.Exists(id))
                return NotFound();

            var model = _event.Adapt<EventServiceModel>();
            model.IsActive = true;

            await _service.UpdateAsync(model);

            return Ok();
        }

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
