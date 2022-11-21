﻿using Api.DB;
using Api.Models.Client;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public class ClientService : IClientService
{
    private ProductRepository _productRepository;
    private readonly IConfiguration _configuration;
    
    public ClientService(IConfiguration configuration)
    {
        _configuration = configuration;
        _productRepository = new ProductRepository(_configuration.GetConnectionString("ProductsAppCon"));
    }
    
    public IActionResult GetClient(int id)
    {
        return _productRepository.GetClient(id);
    }

    public IActionResult GetClient(string token)
    {
        return _productRepository.GetClient(token);
    }

    public IActionResult GetClientOrders(int id)
    {
        return _productRepository.GetClientOrders(id);
    }

    public IActionResult AddClient(Client client)
    {
        return _productRepository.AddClient(client);
    }

    public IActionResult EditClient(Client client)
    {
        return _productRepository.EditClient(client);
    }
    
    public IActionResult DeleteClient(int id)
    {
        return _productRepository.DeleteClient(id);
    }
}