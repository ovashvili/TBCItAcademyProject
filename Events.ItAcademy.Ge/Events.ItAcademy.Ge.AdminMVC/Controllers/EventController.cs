using Events.ItAcademy.Ge.AdminMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Events.ItAcademy.Ge.AdminMVC.Helper.Helper;

namespace Events.ItAcademy.Ge.AdminMVC.Controllers
{
    [Authorize(Policy = "Admin")]
    public class EventController : Controller
    {
        private readonly EventAPI _api = new EventAPI();

        public async Task<IActionResult> Index()
        {
            List<EventViewModel> events = new List<EventViewModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.EventPath());
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                events = JsonConvert.DeserializeObject<List<EventViewModel>>(result);
            }

            return View(events);
        }

        // GET: EventController/Edit/5
        public async Task<ActionResult> Publish(int id)
        {
            var _event = new EventViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.EventPath() + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                _event = JsonConvert.DeserializeObject<EventViewModel>(result);
            }
            return View(_event);
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Publish(int id, EventViewModel _event)
        {
            HttpClient client = _api.Initial();
            _event.EventID = id;
            var responseMessage = await client.PutAsJsonAsync(_api.EventPath() + id, _event);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(_event);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _api.Initial();
            var responseMessage = await client.DeleteAsync(_api.EventPath() + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}
