using Api.Models;
using Api.Models.Product;
using Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[EnableCors("Police1")]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : Controller, IProductsService
{
    private IProductsService _productsService;
    
    public ProductsController(IConfiguration configuration)
    {
        _productsService = new ProductsService(configuration);
    }

    
    [HttpGet("GetProducts")]
    public IActionResult GetProducts()
    {
        return _productsService.GetProducts();
    }
    
    [HttpGet("GetProductById")]
    public IActionResult GetProductById(int id)
    {
        return _productsService.GetProductById(id);
    }
    
    [HttpDelete("DeleteProduct")]
    public IActionResult DeleteProduct(int id)
    {
        return _productsService.DeleteProduct(id);
    }
    
    [HttpDelete("DeleteProduct")]
    public IActionResult DeleteProduct(Product product)
    {
        return _productsService.DeleteProduct(product);
    }
    
    [HttpPost("AddProduct")]
    public IActionResult AddProduct(Product product)
    {
        return _productsService.AddProduct(product);
    }
    
    [HttpPost("UpdateProduct")]
    public IActionResult UpdateProduct(Product product)
    {
        return _productsService.UpdateProduct(product);
    }
    
    [HttpPost("AddProductCategory")]
    public IActionResult AddProductCategory(Category category)
    {
        return _productsService.AddProductCategory(category);
    }

    [HttpPost("EditProductCategory")]
    public IActionResult EditProductCategory(Category category)
    {
        return _productsService.EditProductCategory(category);
    }
    
    [HttpDelete("DeleteProductCategory")]
    public IActionResult DeleteProductCategory(Category category)
    {
        return _productsService.DeleteProductCategory(category);
    }
    
    [HttpPost("AddBrand")]
    public IActionResult AddBrand(Brand brand)
    {
        return _productsService.AddBrand(brand);
    }
    
    [HttpDelete("DeleteBrand")]
    public IActionResult DeleteBrand(Brand brand)
    {
        return _productsService.DeleteBrand(brand);
    }
    
    [HttpPost("EditBrand")]
    public IActionResult EditBrand(Brand brand)
    {
        return _productsService.EditBrand(brand);
    }
    
    [HttpGet("GetProductBrands")]
    public IActionResult GetProductBrands(Product product)
    {
        return _productsService.GetProductBrands(product);
    }
    
    [HttpGet("GetBrands")]
    public IActionResult GetBrands()
    {
        return _productsService.GetBrands();
    }
    
    [HttpPost("AddBrandToProduct")]
    public IActionResult AddBrandToProduct(Product product, Brand brand)
    {
        return _productsService.AddBrandToProduct(product, brand);
    }
    
    [HttpPost("AddBrandsToProduct")]
    public IActionResult AddBrandsToProduct(Product product, List<Brand> brands)
    {
        return _productsService.AddBrandsToProduct(product, brands);
    }
    
    [HttpDelete("DeleteBrandFromProduct")]
    public IActionResult DeleteBrandFromProduct(Product product, Brand brand)
    {
        return _productsService.DeleteBrandFromProduct(product, brand);
    }
    
    [HttpPost("SetProductBrands")]
    public IActionResult SetProductBrands(Product product, List<Brand> brands)
    {
        return _productsService.SetProductBrands(product, brands);
    }
    
    [HttpDelete("ClearProductBrands")]
    public IActionResult ClearProductBrands(Product product)
    {
        return _productsService.ClearProductBrands(product);
    }
    
    [HttpPost("AddProductPhoto")]
    public IActionResult AddProductPhoto(Product product, ProductPhoto productPhoto)
    {
        return _productsService.AddProductPhoto(product, productPhoto);
    }
    
    [HttpDelete("DeleteProductPhoto")]
    public IActionResult DeleteProductPhoto(ProductPhoto productPhoto)
    {
        return _productsService.DeleteProductPhoto(productPhoto);
    }
    
    [HttpPost("EditProductPhoto")]
    public IActionResult EditProductPhoto(ProductPhoto productPhoto)
    {
        return _productsService.EditProductPhoto(productPhoto);
    }
    
    [HttpPost("AddProductPhotos")]
    public IActionResult AddProductPhotos(Product product, List<ProductPhoto> productPhotos)
    {
        return _productsService.AddProductPhotos(product, productPhotos);
    }
    
    [HttpPost("SetProductPhotos")]
    public IActionResult SetProductPhotos(Product product, List<ProductPhoto> productPhotos)
    {
        return _productsService.SetProductPhotos(product, productPhotos);
    }
    
    [HttpDelete("ClearProductPhotos")]
    public IActionResult ClearProductPhotos(Product product)
    {
        return _productsService.ClearProductPhotos(product);
    }
    
    [HttpPost("AddNewProductPrice")]
    public IActionResult AddNewProductPrice(Product product, ProductPrice productPrice)
    {
        return _productsService.AddNewProductPrice(product, productPrice);
    }
    
    [HttpDelete("DeleteProductPrice")]
    public IActionResult DeleteProductPrice(ProductPrice productPrice)
    {
        return _productsService.DeleteProductPrice(productPrice);
    }
}