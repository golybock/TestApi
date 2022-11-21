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
        public IActionResult GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IActionResult AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IActionResult EditCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IActionResult DeleteCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IActionResult DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetCustomerProducts(int id)
        {
            throw new NotImplementedException();
        }
    }
}
