using Api.DB;
using Api.Models.Client;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public class ClientService : IClientService
{
    private readonly ProductRepository _productRepository;

    public ClientService(IConfiguration configuration)
    {
        _productRepository = new ProductRepository(configuration.GetConnectionString("ProductsAppCon"));
    }
    
    public IActionResult GetClient(int id)
    {
        if (id < 1)
            return new BadRequestObjectResult("Id cannot be less than 1");

        return _productRepository.GetClient(id);
    }

    public IActionResult GetClient(string token)
    {
        if (string.IsNullOrEmpty(token))
            return new BadRequestObjectResult("Token cannot be empty");

        return _productRepository.GetClient(token);
    }

    public IActionResult GetClientByLogin(string login)
    {
        if (string.IsNullOrEmpty(login))
            return new BadRequestObjectResult("Login cannot be empty");

        return _productRepository.GetClientByLogin(login);
    }

    public IActionResult GetClientOrders(int id)
    {
        if (id < 1)
            return new BadRequestObjectResult("Id cannot be less than 1");

        return _productRepository.GetClientOrders(id);
    }

    public IActionResult AddClient(Client client)
    {
        if (client.Id < 1)
            return new BadRequestObjectResult("Id cannot be less than 1");

        return _productRepository.AddClient(client);
    }

    public IActionResult EditClient(Client client)
    {
        if (client.Id < 1)
            return new BadRequestObjectResult("Id cannot be less than 1");

        return _productRepository.EditClient(client);
    }
    
    public IActionResult DeleteClient(int id)
    {
        if (id < 1)
            return new BadRequestObjectResult("Id cannot be less than 1");

        return _productRepository.DeleteClient(id);
    }
}