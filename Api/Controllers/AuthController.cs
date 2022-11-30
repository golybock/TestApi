using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models.Client;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;
        private IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _authService = new AuthService(configuration);
        }
        
        [HttpPost, Route("login")]
        public IActionResult Login(string login, string password, string key)
        {
            try
            {
                if (AuthDataIsNull(login, password, key))
                    return BadRequest("Username and/or Password, key not specified");

                if (KeyIsValid(key))
                    return BadRequest("Key is not valid");
                
                if (_authService.ClientAuthDataValid(login, password))
                {
                    var secretKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signinCredentials = new SigningCredentials
                        (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: new List<Claim> {new (ClaimTypes.Email, login)},
                        expires: DateTime.UtcNow.AddHours(100),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
                }
            }
            catch(Exception ex)
            {
                return BadRequest
                    ("An error occurred in generating the token" + ex);
            }
            return Unauthorized();
        }

        public bool AuthDataIsNull(string login, string pass, string key)
        {
            return string.IsNullOrEmpty(login) ||
                   string.IsNullOrEmpty(pass) ||
                   string.IsNullOrEmpty(key);
        }

        public bool KeyIsValid(string key)
        {
            // сравниваем секретный ключ с полученым
            return _configuration["AppSettings:Secret"] == key;
        }
        
    }
}
