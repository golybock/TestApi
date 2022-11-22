using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Customer;
using Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [EnableCors("Police1")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase, ICustomerService
    {    
        private CustomerService _customerService;
    
        public CustomerController(IConfiguration configuration)
        {
            _customerService = new CustomerService(configuration);
        }
        
        [HttpGet("GetCustomer")]
        public IActionResult GetCustomer(int id)
        {
            return _customerService.GetCustomer(id);
        }
        
        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer(Customer customer)
        {
            return _customerService.AddCustomer(customer);
        }
        
        [HttpPost("EditCustomer")]
        public IActionResult EditCustomer(Customer customer)
        {
            return _customerService.EditCustomer(customer);
        }
        
        [HttpPost("DeleteCustomer")]
        public IActionResult DeleteCustomer(int id)
        {
            return _customerService.DeleteCustomer(id);
        }
        
        [HttpGet("GetCustomerProducts")]
        public IActionResult GetCustomerProducts(int id)
        {
            return _customerService.GetCustomerProducts(id);
        }
    }
}
