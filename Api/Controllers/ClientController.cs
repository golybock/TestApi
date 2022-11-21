using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Client;
using Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [EnableCors("Police1")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase, IClientService
    {
        private ClientService _clientService;
    
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
}
