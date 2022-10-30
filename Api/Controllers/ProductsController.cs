using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[EnableCors("Police1")]
[Route("[controller]")]
[ApiController]
public class ProductsController : Controller, IProductsService
{
    private IProductsService _productsService;
    public ProductsController(IConfiguration configuration)
    {
        _productsService = new ProductsService(configuration);
    }
        
    [HttpGet("GetProducts")]
    public List<Product> GetProducts()
    {
        return _productsService.GetProducts();
    }
        
    [HttpGet("GetProductById")]
    public Product GetProductById(int id)
    {
        return _productsService.GetProductById(id);
    }
        
    [HttpPost("DeleteProduct")]
    public IActionResult DeleteProduct(int productId)
    {
        return _productsService.DeleteProduct(productId);
    }
        
    [HttpPost("AddProduct")]
    public IActionResult AddProduct([FromBody] Product product)
    {
        return _productsService.AddProduct(product);
    }
        
    [HttpPost("UpdateProduct")]
    public IActionResult UpdateProduct([FromBody] Product product)
    {
        return _productsService.UpdateProduct(product);
    }
        
}