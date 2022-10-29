using Microsoft.AspNetCore.Mvc;
using TestApi.Models;

namespace TestApi.Services;

public interface IProductsService
{
    List<Product> GetProducts();
    Product GetProductById(int id);
    IActionResult DeleteProduct(int id);
    IActionResult AddProduct(Product product);
    IActionResult UpdateProduct(Product product);
}