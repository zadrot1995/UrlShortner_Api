using Domain.Models;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using System.Security.Claims;

namespace URL_Shortener.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;
        private readonly IUserService _userService;
        public UrlController(IUrlService urlService, IUserService userService)
        {
            _urlService = urlService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var result = (await _urlService.Get()).ToList();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Url url)
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    url.CreatorId = new Guid(userIdClaim.Value);
                }
            }

            if (User.Identity.IsAuthenticated)
            {
                var creator = User.Identity;
                if (creator != null)
                {
                    _urlService.Insert(url);
                }
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id.GetValueOrDefault() != Guid.Empty && id != null)
            {
                await _urlService.Delete(id.GetValueOrDefault());
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                Url? url = _urlService.GetById(id.GetValueOrDefault());
                if(url != null)
                {
                    return View(url);
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> GetById(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                Url? url = _urlService.GetById(id.GetValueOrDefault());
                if (url != null)
                {
                    return View(url);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Url url)
        {
            _urlService.Update(url);
            return RedirectToAction("Index");
        }
    }
}
