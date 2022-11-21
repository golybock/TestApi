using Api.DB;
using Api.Models.Client;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public class ClientService : IClientService
{
    private ProductRepository _productRepository;
    private readonly IConfiguration _configuration;
    
    public ClientService(IConfiguration configuration)
    {
        _configuration = configuration;
        _productRepository = new ProductRepository(_configuration.GetConnectionString("ProductsAppCon"));
    }
    
    public IActionResult GetClient(int id)
    {
        throw new NotImplementedException();
    }

    public IActionResult GetClient(string token)
    {
        throw new NotImplementedException();
    }

    public IActionResult GetClientOrders(int id)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddClient(Client client)
    {
        throw new NotImplementedException();
    }

    public IActionResult EditClient(Client client)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteClient(Client client)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteClient(int id)
    {
        throw new NotImplementedException();
    }
}