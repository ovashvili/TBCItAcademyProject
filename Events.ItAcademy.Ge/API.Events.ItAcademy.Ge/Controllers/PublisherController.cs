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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _service;
        public PublisherController(IPublisherService service)
        {
            _service = service;
        }

        // GET: api/<PublisherController>
        [HttpGet]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task<IActionResult> GetAsync()
        {
            var serviceResult = await _service.GetAllAsync();

            return Ok(serviceResult.Adapt<List<PublisherDTO>>());
        }

        // GET api/<PublisherController>/5
        [HttpGet("{id}", Name = "GetPublisher")]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var serviceResult = await _service.GetAsync(id);

            if (EqualityComparer<PublisherServiceModel>.Default.Equals(serviceResult, default))
                return NotFound();

            var response = serviceResult.Adapt<PublisherDTO>();

            return Ok(response);
        }

        // POST api/<PublisherController>
        [HttpPost]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task<int> PostAsync([FromBody] PublisherDTO publisher)
        {
            var model = publisher.Adapt<PublisherServiceModel>();

            return await _service.AddAsync(model);
        }

        // PUT api/<PublisherController>/5
        [HttpPut("{id}")]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] PublisherDTO publisher)
        {

            if (!await _service.Exists(id))
                return NotFound();
            
            var model = publisher.Adapt<PublisherServiceModel>();

            await _service.UpdateAsync(model);
            
            return Ok();
        }

        // DELETE api/<PublisherController>/5
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
