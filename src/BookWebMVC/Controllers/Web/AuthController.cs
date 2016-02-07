using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebMVC.Data.Model;
using BookWebMVC.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookWebMVC.Controllers.Web
{
    public class AuthController : Controller
    {
        private readonly SignInManager<BookWebUser> _signInManager;

        public AuthController(SignInManager<BookWebUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Nav = "Login";

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserProfile", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.Remember, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserProfile", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
