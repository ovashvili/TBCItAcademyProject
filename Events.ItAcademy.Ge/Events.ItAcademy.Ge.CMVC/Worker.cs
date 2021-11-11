using Events.ItAcademy.Ge.CMVC.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static Events.ItAcademy.Ge.CMVC.Helper.Helper;

namespace Events.ItAcademy.Ge.CMVC
{
    public class Worker : IWorker
    {
        private readonly ILogger<Worker> _logger;
        private readonly EventAPI _api = new EventAPI();
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        public async Task DoWorkAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                List<EventViewModel> events = new List<EventViewModel>();
                HttpClient client = _api.Initial();
                HttpResponseMessage responseMessage = await client.GetAsync(_api.EventPath());
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    events = JsonConvert.DeserializeObject<List<EventViewModel>>(result);
                }

                foreach (var _event in events)
                {
                    if (_event.Date < DateTime.Now)
                    {
                        _event.IsArchived = true;
                        _event.IsActive = true;
                        await client.PutAsJsonAsync(_api.EventPath() + _event.EventID, _event);
                    }
                }

                _logger.LogInformation("Printing from worker");
                await Task.Delay(1000 * 5);
            }
        }
    }
}
