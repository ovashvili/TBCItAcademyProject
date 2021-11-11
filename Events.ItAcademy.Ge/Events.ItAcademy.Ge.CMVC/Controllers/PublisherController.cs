using Events.ItAcademy.Ge.CMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Events.ItAcademy.Ge.CMVC.Helper.Helper;

namespace Events.ItAcademy.Ge.CMVC.Controllers
{
    [Authorize(Policy = "User")]
    public class PublisherController : Controller
    {
        readonly EventAPI _api = new EventAPI();
        // GET: PublisherController
        public async Task<ActionResult> Index()
        {
            List<PublisherViewModel> publishers = new List<PublisherViewModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.PublisherPath());
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                publishers = JsonConvert.DeserializeObject<List<PublisherViewModel>>(result);
            }
            return View(publishers);
        }

        // GET: PublisherController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var publisher = new PublisherViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.PublisherPath() + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                publisher = JsonConvert.DeserializeObject<PublisherViewModel>(result);
            }
            return View(publisher);
        }


        // GET: PublisherController/Delete/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var publisher = new PublisherViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.PublisherPath() + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                publisher = JsonConvert.DeserializeObject<PublisherViewModel>(result);
                return View(publisher);
            }

            return NotFound();
        }

        // POST: PublisherController/Delete/5
        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = _api.Initial();
            var responseMessage = await client.DeleteAsync(_api.PublisherPath() + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
