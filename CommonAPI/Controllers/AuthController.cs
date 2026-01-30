using Common.BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommonAPI.Controllers
{
    [Route("/students/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        public AuthController(ITokenService tokenService)
        {
            this._tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var claims = new List<Claim>
            { 
                new Claim(ClaimTypes.NameIdentifier, "101"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("permission", "student.delete")
            };
           var token =  _tokenService.GenerateToken(claims);
            return Ok( token );

        }
    }
}
