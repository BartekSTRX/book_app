using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebMVC.Data.Model;
using BookWebMVC.ViewModels.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookWebMVC.Controllers.Web
{
    public class AuthController : Controller
    {
        private readonly SignInManager<BookWebUser> _signInManager;
        private readonly UserManager<BookWebUser> _userManager;

        public AuthController(SignInManager<BookWebUser> signInManager, UserManager<BookWebUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var newUser = new BookWebUser {UserName = vm.Username, Email = vm.Email};
                var result = await _userManager.CreateAsync(newUser, vm.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("UserProfile", "Home");
                }

                foreach (var error in result.Errors)
                {                
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(vm);
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

        [HttpGet]
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
