using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Models.Client;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private AuthService _authService;

        public LoginController(IConfiguration configuration)
        {
            _authService = new AuthService(configuration);
        }
        
        [HttpPost, Route("login")]
        public IActionResult Login(Client loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.Email) ||
                    string.IsNullOrEmpty(loginDTO.Password))
                    return BadRequest("Username and/or Password not specified");
                
                if (_authService.ClientAuthDataValid(loginDTO))
                {
                    var secretKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes("abosetdryftugyihujiko';ilukgyfjhdgsfdfghjba"));
                    var signinCredentials = new SigningCredentials
                        (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: "aboba",
                        audience: "aboba",
                        claims: new List<Claim>() {new Claim(ClaimTypes.Name, loginDTO.Email)},
                        expires: DateTime.UtcNow.AddHours(100),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
                }
            }
            catch(Exception ex)
            {
                return BadRequest
                    ("An error occurred in generating the token" + ex.ToString());
            }
            return Unauthorized();
        }
    }
}
