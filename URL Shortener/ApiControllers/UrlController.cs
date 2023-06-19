using Domain.DTOs;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace User_Shortener.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Url>>> GetUrls()
        {
            var result = await _urlService.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Url>> GetUrl(Guid id)
        {
            var result = _urlService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUrl([FromBody] UrlCreatingModel urlDTO)
        {
            var url = new Url { LongUrl = urlDTO.LongUrl, CreatorId = urlDTO.Creator };


            _urlService.Insert(url);
            return Ok();
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id.GetValueOrDefault() != Guid.Empty && id != null)
            {
                await _urlService.Delete(id.GetValueOrDefault());
                return NoContent();
            }
            return NotFound();
        }
    }
}
