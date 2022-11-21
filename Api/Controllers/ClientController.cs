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
}
