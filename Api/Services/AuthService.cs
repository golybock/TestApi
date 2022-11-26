using Api.DB;
using Api.Models.Client;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public class AuthService
{
    private ProductRepository _productRepository;
    private readonly IConfiguration _configuration;
    
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
        _productRepository = new ProductRepository(_configuration.GetConnectionString("ProductsAppCon"));
    }

    public bool ClientAuthDataValid(string login, string password)
    {
        OkObjectResult ok = _productRepository.GetClientByLogin(login) as OkObjectResult;
        
        var cl = ok.Value as Client;
        
        if (cl.Id != 0)
        {
            return cl.Password == password;
        }
        return false;
    }
}