using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IProductsService
{
    List<Product> GetProducts();
    Product GetProductById(int id);
    IActionResult DeleteProduct(int id);
    IActionResult AddProduct(Product product);
    IActionResult UpdateProduct(Product product);
}