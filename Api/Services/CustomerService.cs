using Api.Database;
using Api.Models.Customer;
using Api.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public class CustomerService : ICustomerService
{
    private readonly ProductRepository _productRepository;
    private readonly IConfiguration _configuration;
    
    public CustomerService(IConfiguration configuration)
    {
        _configuration = configuration;
        _productRepository = new ProductRepository(_configuration.GetConnectionString("ProductsAppCon"));
    }
    
    public IActionResult GetCustomer(int id)
    {
        return _productRepository.GetCustomer(id);
    }

    public IActionResult AddCustomer(Customer customer)
    {
        return _productRepository.AddCustomer(customer);
    }

    public IActionResult EditCustomer(Customer customer)
    {
        return _productRepository.EditCustomer(customer);
    }

    public IActionResult DeleteCustomer(int id)
    {
        return _productRepository.DeleteCustomer(id);
    }

    public IActionResult GetCustomerProducts(int id)
    {
        return _productRepository.GetCustomerProducts(id);
    }
}