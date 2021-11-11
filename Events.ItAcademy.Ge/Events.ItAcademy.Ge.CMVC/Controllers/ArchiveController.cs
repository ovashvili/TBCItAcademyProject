using Events.ItAcademy.Ge.CMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Events.ItAcademy.Ge.CMVC.Helper.Helper;

namespace Events.ItAcademy.Ge.CMVC.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly ILogger<ArchiveController> _logger;
        private readonly EventAPI _api = new EventAPI();

        public ArchiveController(ILogger<ArchiveController> logger)
        {
            _logger = logger;
        }

        // GET: ArchiveController
        public async Task<ActionResult> Index(int pg = 1)
        {
            List<EventViewModel> events = new List<EventViewModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.ArchivePath());
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                events = JsonConvert.DeserializeObject<List<EventViewModel>>(result);
            }


            const int pageSize = 6;
            if (pg < 1)
                pg = 1;

            int totalEvents = events.Count();

            var pager = new Pager(totalEvents, pg, pageSize);

            int eventsSkip = (pg - 1) * pageSize;

            var data = events.Skip((pg - 1) * pageSize).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        // GET: ArchiveController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var _event = new EventViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.ArchivePath() + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                _event = JsonConvert.DeserializeObject<EventViewModel>(result);
            }

            return View(_event);
        }

        // GET: ArchiveController/Delete/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var _event = new EventViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.ArchivePath() + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                _event = JsonConvert.DeserializeObject<EventViewModel>(result);
                return View(_event);
            }

            return NotFound();
        }

        // POST: ArchiveController/Delete/5
        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = _api.Initial();
            var responseMessage = await client.DeleteAsync(_api.ArchivePath() + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
