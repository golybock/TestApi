using Api.Models.Client;
using Api.Services;
using Api.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[EnableCors("Police")]
[Route("api/[controller]")]
[ApiController, Authorize]
public class ClientController : ControllerBase, IClientService
{
    private readonly ClientService _clientService;

    public ClientController(IConfiguration configuration)
    {
        _clientService = new ClientService(configuration);
    }
    
    [HttpGet("GetClientById")]
    public IActionResult GetClient(int id)
    {
        return _clientService.GetClient(id);
    }
    
    [HttpGet("GetClientByToken")]
    public IActionResult GetClient(string token)
    {
        return _clientService.GetClient(token);
    }
    
    [HttpGet("GetClientByLogin")]
    public IActionResult GetClientByLogin(string login)
    {
        return _clientService.GetClientByLogin(login);
    }

    [HttpGet("GetClientOrders")]
    public IActionResult GetClientOrders(int id)
    {
        return _clientService.GetClientOrders(id);
    }
    
    [HttpPost("AddClient")]
    public IActionResult AddClient(Client client)
    {
        return _clientService.AddClient(client);
    }
    
    [HttpPost("EditClient")]
    public IActionResult EditClient(Client client)
    {
        return _clientService.EditClient(client);
    }
    
    [HttpPost("DeleteClient")]
    public IActionResult DeleteClient(int id)
    {
        return _clientService.DeleteClient(id);
    }
}

