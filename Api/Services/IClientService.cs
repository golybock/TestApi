using Api.Models.Client;
using Api.Models.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IClientService
{
    public IActionResult GetClient(int id);
    public IActionResult GetClient(string token);
    public IActionResult GetClientOrders(Client client);
    public IActionResult AddClient(Client client);
    public IActionResult EditClient(Client client);
    public IActionResult DeleteClient(Client client);
    public IActionResult DeleteClient(int id);
}