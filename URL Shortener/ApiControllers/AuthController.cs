using Domain;
using Domain.Dtos;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly ITokenService _tokenService;
        public AuthController(ApplicationContext context, ITokenService tokenService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser()
        {
            //var role = this.User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;

            var user = _context.Users.Where(x => x.Login == HttpContext.User.Identity.Name).FirstOrDefault();

            return Ok(new { login = this.User.Identity.Name, id = user.Id });
        }

        [HttpGet("getUserRole"), Authorize]
        public async Task<IActionResult> GetUserRole()
        {
            var role = this.User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;
            return Ok(new { userType = role });
        }



        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginDTO loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }
            var user = _context.Users.FirstOrDefault(u =>
                (u.Login == loginModel.Login) && (u.Password == loginModel.Password));
            if (user is null)
                return Unauthorized();
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, loginModel.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

            };


            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            _context.SaveChanges();



            ClaimsIdentity id = new ClaimsIdentity(claims);
            HttpContext.SignInAsync("JwtBearer", new ClaimsPrincipal(id));

            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO is null)
            {
                return BadRequest("Invalid client request");
            }

            var id = Guid.NewGuid();
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, registerDTO.Login),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),

            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            User newUser = new User
            {
                Id = id,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(7),
                UserType = UserType.User,
                Created = DateTime.UtcNow,
                LastUpdate = DateTime.UtcNow,
                Login = registerDTO.Login,
                Password = registerDTO.Password
            };


            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}