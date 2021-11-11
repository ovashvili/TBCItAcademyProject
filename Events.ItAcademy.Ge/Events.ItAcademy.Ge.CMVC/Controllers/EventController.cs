using Events.ItAcademy.Ge.CMVC.Models;
using Events.ItAcademy.Ge.PersistenceDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Events.ItAcademy.Ge.CMVC.Helper.Helper;

namespace Events.ItAcademy.Ge.CMVC.Controllers
{
    public class EventController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EventController> _logger;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly EventAPI _api = new EventAPI();

        public EventController(UserManager<ApplicationUser> userManager, ILogger<EventController> logger, IWebHostEnvironment webHostEnviroment)
        {
            _userManager = userManager;
            _logger = logger;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: EventController
        public async Task<ActionResult> Index(int pg = 1)
        {
            List<EventViewModel> events = new List<EventViewModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.EventPath());
            if(responseMessage.IsSuccessStatusCode)
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

        // GET: EventController/Details/5
        public async Task<ActionResult> Details(int id)
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

        // GET: EventController/Create

        [Authorize(Policy = "User")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventController/Create
        [Authorize(Policy = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventViewModel _event)
        {
            _event.PublisherID = await AddPublisher();

            string photoPath = null;

            if(_event.Photo != null)
            {
                string uploadPath = Path.Combine(_webHostEnviroment.WebRootPath, "img");
                photoPath = Guid.NewGuid().ToString() + "_" + _event.Photo.FileName;
                _event.Photo.CopyTo(new FileStream(Path.Combine(uploadPath, photoPath), FileMode.Create));
            }
            
            _event.PhotoPath = photoPath;

            HttpClient client = _api.Initial();

            var result = await client.PostAsJsonAsync(_api.EventPath(), _event);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
         
            return View(_event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<int> AddPublisher()
        {
            HttpClient client = _api.Initial();

            var user = await _userManager.GetUserAsync(HttpContext.User);

            HttpResponseMessage responseMessage = await client.GetAsync(_api.PublisherPath());

            var publishers = JsonConvert.DeserializeObject<List<PublisherViewModel>>(await responseMessage.Content.ReadAsStringAsync());

            var publisher = publishers.FirstOrDefault(p => p.Email == user.Email);

            if (publisher != null)
                return publisher.PublisherID;

            client = _api.Initial();

            publisher = new PublisherViewModel
            {
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
            };

            var result = await client.PostAsJsonAsync(_api.PublisherPath(), publisher);

            int.TryParse(await result.Content.ReadAsStringAsync(), out int id);

            return id;
        }

        // GET: EventController/Edit/5
        [Authorize(Policy = "User")]
        public async Task<ActionResult> Edit(int id)
        {
            var _event = new EventViewModel();
            
            HttpClient client = _api.Initial();

            HttpResponseMessage responseMessage = await client.GetAsync(_api.EventPath() + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                _event = JsonConvert.DeserializeObject<EventViewModel>(result);
            }

            var user = await _userManager.GetUserAsync(User);

            var roles = new List<string>(await _userManager.GetRolesAsync(user));

            if (!(roles.Contains("Admin") || user.Email == _event.Publisher.Email) || _event.ModifiableTill < DateTime.Now)
                return RedirectToAction("Index", "Event");

            return View(_event);
        }

        // POST: EventController/Edit/5
        [Authorize(Policy = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventViewModel _event)
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

        // GET: EventController/Delete/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var _event = new EventViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.EventPath() + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                _event = JsonConvert.DeserializeObject<EventViewModel>(result);
                return View(_event);
            }

            return NotFound();
        }

        // POST: EventController/Delete/5
        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = _api.Initial();
            var responseMessage = await client.DeleteAsync(_api.EventPath() + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
