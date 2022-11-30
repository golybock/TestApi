using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.DB;
using Api.Models.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class AuthService
{
    private readonly ProductRepository _productRepository;
    private readonly IConfiguration _configuration;
    
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
        _productRepository = new ProductRepository(_configuration.GetConnectionString("ProductsAppCon"));
    }
    
    public IActionResult Login(string login, string password, string key)
    {
        try
        {
            if (AuthDataIsNull(login, password, key))
                return new BadRequestObjectResult("Username and/or Password, key not specified");

            if (!KeyIsValid(key))
                return new BadRequestObjectResult("Key is not valid");

            string token = GenerateToken(
                new List<Claim> {
                    new(ClaimTypes.Email, login),
                    new (ClaimTypes.UserData, DateTime.UtcNow.ToString())
                    
                });
            
            if (ClientAuthDataValid(login, password))
            {
                return new OkObjectResult(token);
            }
        }
        catch(Exception ex)
        {
            return new BadRequestObjectResult("An error occurred in generating the token" + ex);
        }
        return new UnauthorizedResult();
    }

    private bool ClientAuthDataValid(string login, string password)
    {
        OkObjectResult? ok = _productRepository.GetClientByLogin(login) as OkObjectResult;

        if (ok?.Value is Client cl && cl.Id != 0)
            return cl.Password == password;
        
        return false;
    }

    private bool AuthDataIsNull(string login, string pass, string key)
    {
        return string.IsNullOrEmpty(login) ||
               string.IsNullOrEmpty(pass) ||
               string.IsNullOrEmpty(key);
    }

    private bool KeyIsValid(string key)
    {
        // сравниваем секретный ключ с полученым
        return _configuration["AppSettings:Secret"] == key;
    }

    private string GenerateToken(List<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    
        var signinCredentials = new SigningCredentials
            (secretKey, SecurityAlgorithms.HmacSha256);

        var issuer = _configuration["Jwt:Issuer"];

        var audience = _configuration["Jwt:Audience"];
                    
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            signingCredentials: signinCredentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
    
}