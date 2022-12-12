using Api.Models.Customer;
using Api.Services;
using Api.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[EnableCors("Police")]
[Route("api/[controller]")]
[ApiController, Authorize]
public class CustomerController : ControllerBase, ICustomerService
{    
    private readonly CustomerService _customerService;

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

