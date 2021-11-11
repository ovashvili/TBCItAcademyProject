using Events.ItAcademy.Ge.AdminMVC.Models;
using Events.ItAcademy.Ge.PersistenceDB.Enums;
using Events.ItAcademy.Ge.PersistenceDB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Events.ItAcademy.Ge.AdminMVC.Helper.Helper;

namespace Events.ItAcademy.Ge.AdminMVC.Controllers
{
    public class RegisterUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<RegisterUserController> _logger;
        private readonly EventAPI _api = new EventAPI();

        public RegisterUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<RegisterUserController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }



    }
}
