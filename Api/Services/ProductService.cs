using Api.DB;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public class ProductsService : IProductsService
{
    private ProductRepository _productRepository;
    private readonly IConfiguration _configuration;
    
    public ProductsService(IConfiguration configuration)
    {
        _configuration = configuration;
        _productRepository = new ProductRepository(_configuration.GetConnectionString("ProductsAppCon"));
    }

    public List<Product> GetProducts()
    {
        return _productRepository.GetProducts();
    }

    public Product GetProductById(int id)
    {
        return _productRepository.GetProductById(id);
    }
    
    public IActionResult DeleteProduct(int id)
    {
        return _productRepository.DeleteProduct(id);
    }

    public IActionResult AddProduct(Product product)
    {
        return _productRepository.AddProduct(product);
    }

    public IActionResult UpdateProduct(Product product)
    {
        return _productRepository.UpdateProduct(product);
    }

    public List<ProductCategory> GetCategories()
    {
        return _productRepository.GetCategories();
    }

    public IActionResult AddCategory(ProductCategory productCategory)
    {
        return _productRepository.AddCategory(productCategory);
    }
}