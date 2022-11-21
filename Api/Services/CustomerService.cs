using Api.DB;
using Api.Models.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public class CustomerService : ICustomerService
{
    private ProductRepository _productRepository;
    private readonly IConfiguration _configuration;
    
    public CustomerService(IConfiguration configuration)
    {
        _configuration = configuration;
        _productRepository = new ProductRepository(_configuration.GetConnectionString("ProductsAppCon"));
    }
    
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