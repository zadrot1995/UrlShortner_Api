using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Domain.Models;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AuthApp.Controllers
{
    [System.Web.Mvc.HandleError]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                User user = (await _userService.Get()).FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);

                if (user != null)
                {
                    await Authenticate(model.Login, user.Id.ToString());

                    return RedirectToAction("Index", "Url");
                }
                ModelState.AddModelError("", "Uncorrect data");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                User user = (await _userService.Get()).FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                if (user == null)
                {
                    var newUser = new User { Login = model.Login, Password = model.Password, Id = Guid.NewGuid() };
                    _userService.Insert(newUser);

                    await Authenticate(model.Login, newUser.Id.ToString());

                    return RedirectToAction("Index", "url");
                }
                else
                    ModelState.AddModelError("", "Incorrect data");
            }
            return View(model);
        }

        private async Task Authenticate(string userName, string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimTypes.NameIdentifier, userId),

            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Url");
        }
    }
}