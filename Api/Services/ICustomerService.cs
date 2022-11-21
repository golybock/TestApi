using Api.Models.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface ICustomerService
{
    public IActionResult GetCustomer(int id);
    public IActionResult AddCustomer(Customer customer);
    public IActionResult EditCustomer(Customer customer);
    public IActionResult DeleteCustomer(int id);
    public IActionResult GetCustomerProducts(int id);
}