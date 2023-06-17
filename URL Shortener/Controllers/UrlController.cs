using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;

namespace URL_Shortener.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;
        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _urlService.Get().ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Url url)
        {
            _urlService.Insert(url);
            return RedirectToAction("Index");
        }
    }
}
