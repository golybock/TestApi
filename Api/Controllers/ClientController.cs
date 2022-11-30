using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Client;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
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
            if (id < 1)
                return BadRequest("Id cannot be less than 1");
            
            return _clientService.GetClient(id);
        }
        
        [HttpGet("GetClientByToken")]
        public IActionResult GetClient(string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token cannot be empty");
            
            return _clientService.GetClient(token);
        }
        
        [HttpGet("GetClientByLogin")]
        public IActionResult GetClientByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return BadRequest("Login cannot be empty");
            
            return _clientService.GetClientByLogin(login);
        }

        [HttpGet("GetClientOrders")]
        public IActionResult GetClientOrders(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be less than 1");

            return _clientService.GetClientOrders(id);
        }
        
        [HttpPost("AddClient")]
        public IActionResult AddClient(Client client)
        {
            if (client.Id < 1)
                return BadRequest("Id cannot be less than 1");
            
            return _clientService.AddClient(client);
        }
        
        [HttpPost("EditClient")]
        public IActionResult EditClient(Client client)
        {
            if (client.Id < 1)
                return BadRequest("Id cannot be less than 1");

            return _clientService.EditClient(client);
        }
        
        [HttpPost("DeleteClient")]
        public IActionResult DeleteClient(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be less than 1");
            
            return _clientService.DeleteClient(id);
        }
    }
}
