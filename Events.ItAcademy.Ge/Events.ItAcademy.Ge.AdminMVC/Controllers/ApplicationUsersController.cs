using Events.ItAcademy.Ge.AdminMVC.Models;
using Events.ItAcademy.Ge.PersistenceDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class ApplicationUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ApplicationUsersController> _logger;
        readonly EventAPI _api = new EventAPI();

        public ApplicationUsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<ApplicationUsersController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // GET: ApplicationUserController
        public ActionResult List()
        {
            var users = _userManager.Users;
            return View(users);
        }

        // GET: ApplicationUserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplicationUserController/Edit/5
        public async Task<ActionResult> EditAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = userRoles,
                Claims = userClaims.Select(c => c.Value).ToList()
            };

            return View(model);
        }

        // POST: ApplicationUserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                await UpdatePublisher(user.Email, model);
                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        public async Task UpdatePublisher(string email, EditUserViewModel model)
        {
            List<PublisherViewModel> publishers = new List<PublisherViewModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.PublisherPath());
            if (responseMessage.IsSuccessStatusCode)
            {
                publishers = JsonConvert.DeserializeObject<List<PublisherViewModel>>(await responseMessage.Content.ReadAsStringAsync());
                var publisher = publishers.SingleOrDefault(p => p.Email == email);
                if(publisher != null)
                {
                    publisher.Email = model.Email;
                    publisher.Name = $"{model.FirstName} {model.LastName}";
                    await _api.Initial().PutAsJsonAsync(_api.PublisherPath() + publisher.PublisherID, publisher);
                }
            }
        }
        public async Task DeletePublisher(string email)
        {
            List<PublisherViewModel> publishers = new List<PublisherViewModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync(_api.PublisherPath());
            if (responseMessage.IsSuccessStatusCode)
            {
                publishers = JsonConvert.DeserializeObject<List<PublisherViewModel>>(await responseMessage.Content.ReadAsStringAsync());
                var publisher = publishers.SingleOrDefault(p => p.Email == email);
                if (publisher != null)
                {
                    client = _api.Initial();
                    var result = await client.DeleteAsync(_api.PublisherPath() + publisher.PublisherID);
                }
            }
        }



        // GET: ApplicationUserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                await DeletePublisher(user.Email);
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("List");
            }
        }
    }
}
