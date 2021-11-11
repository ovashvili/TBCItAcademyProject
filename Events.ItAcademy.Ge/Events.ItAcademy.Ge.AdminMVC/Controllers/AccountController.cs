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
using System.Security.Claims;
using System.Threading.Tasks;
using static Events.ItAcademy.Ge.AdminMVC.Helper.Helper;

namespace Events.ItAcademy.Ge.AdminMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly EventAPI _api = new EventAPI();
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
                
                if (result.Succeeded)
                {
                    var user = _userManager.Users.SingleOrDefault(u => u.Email == loginModel.Email);

                    var roles = new List<string>(await _userManager.GetRolesAsync(user));
                    
                    if (roles.Contains("Admin"))
                        return RedirectToAction("Index", "Home");


                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Login", "Account");
                }

                
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(loginModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                //MailAddress address = new MailAddress(registerModel.Email);
                //string userName = address.User;
                var user = new ApplicationUser
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    UserName = registerModel.Email,
                    Email = registerModel.Email
                };
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, role: Roles.Basic.ToString());
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerModel);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
