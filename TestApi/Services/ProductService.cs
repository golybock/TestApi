using Microsoft.AspNetCore.Mvc;
using TestApi.DB;
using TestApi.Models;

namespace TestApi.Services;

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
        return _productRepository.GetAllProducts();
    }

    public Product GetProductById(int id)
    {
        return _productRepository.GetProductById(id);
    }
    
    public IActionResult DeleteProduct(int id)
    {

        int result = _productRepository.DeleteProduct(id);
        return result > 0 ? new AcceptedResult() : new BadRequestResult();
    }

    public IActionResult AddProduct(Product product)
    {
        bool result = _productRepository.AddProduct(product);
        return result ? new AcceptedResult() : new BadRequestResult();
    }

    public IActionResult UpdateProduct(Product product)
    {
        int result = _productRepository.UpdateProduct(product);
        return result > 0 ? new AcceptedResult() : new BadRequestResult();
    }

}