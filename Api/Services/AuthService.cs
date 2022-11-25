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

    public bool ClientAuthDataValid(Client client)
    {
        OkObjectResult okemail = _productRepository.GetClientByLogin(client.Email) as OkObjectResult;
        OkObjectResult okphone = _productRepository.GetClientByLogin(client.PhoneNumber) as OkObjectResult;

        var cl = okemail.Value as Client;
        var cl1 = okphone.Value as Client;
        if (cl.Id != 0)
        {
            return cl.Password == client.Password;
        }
        else if (cl1.Id != 0)
        {
            return cl1.Password == client.Password;
        }
        else
        {
            return false;
        }
    }
}