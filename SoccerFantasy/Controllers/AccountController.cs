using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerFantasy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Models.User> userManager;
        private SignInManager<Models.User> signInManager;

        public AccountController(UserManager<Models.User> userManager, SignInManager<Models.User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            //login logic
            var user = await userManager.FindByNameAsync(username);

            if (user != null && await userManager.CheckPasswordAsync(user, password))
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            // Registration logic
            var user = new Models.User
            {
                username = username,
                totalPoints = 0,
                currentRoundPoints = 0
                // Add other properties as needed
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();

        }



        public async Task<IActionResult> SwitchAccount(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if(user != null)
            {
                await signInManager.SignOutAsync();
                await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");

            }

            //Handle error is user not found
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}

