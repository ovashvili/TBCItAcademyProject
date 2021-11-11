using Events.ItAcademy.Ge.CMVC.Models;
using Microsoft.Extensions.Hosting;
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
    public class BackgroundArchiver : IHostedService
    {
        private readonly ILogger<BackgroundArchiver> _logger;
        private readonly IWorker _worker;
        public BackgroundArchiver(ILogger<BackgroundArchiver> logger, IWorker worker)
        {
            _logger = logger;
            _worker = worker;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _worker.DoWorkAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Printing worker stopping");

            return Task.CompletedTask;
        }
    }
}
