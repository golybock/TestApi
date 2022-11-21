using Api.Models;
using Api.Models.Product;
using Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[EnableCors("Police1")]
[Route("[controller]")]
[ApiController]
public class ProductsController : Controller, IProductsService
{
    private ProductsService _productsService;
    
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
    
    [HttpPost("DeleteProductById")]
    public IActionResult DeleteProduct(int id)
    {
        return _productsService.DeleteProduct(id);
    }
    
    [HttpPost("DeleteProductByObject")]
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
    
    [HttpGet("GetCategory")]
    public IActionResult GetCategory(int id)
    {
        return _productsService.GetCategory(id);
    }
    
    [HttpGet("GetCategories")]
    public IActionResult GetCategories()
    {
        return _productsService.GetCategories();
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
    
    [HttpPost("DeleteProductCategory")]
    public IActionResult DeleteProductCategory(Category category)
    {
        return _productsService.DeleteProductCategory(category);
    }
    
    [HttpPost("AddBrand")]
    public IActionResult AddBrand(Brand brand)
    {
        return _productsService.AddBrand(brand);
    }
    
    [HttpPost("DeleteBrand")]
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
    public IActionResult AddBrandToProduct(ProductBrand productBrand)
    {
        return _productsService.AddBrandToProduct(productBrand);
    }

    [HttpPost("AddBrandsToProduct")]
    public IActionResult AddBrandsToProduct(List<ProductBrand> productBrands)
    {
        return _productsService.AddBrandsToProduct(productBrands);
    }
    
    [HttpPost("DeleteBrandFromProduct")]
    public IActionResult DeleteBrandFromProduct(ProductBrand productBrand)
    {
        return _productsService.DeleteBrandFromProduct(productBrand);
    }

    [HttpPost("SetProductBrands")]
    public IActionResult SetProductBrands(List<ProductBrand> productBrands)
    {
        return _productsService.SetProductBrands(productBrands);
    }
    
    [HttpPost("ClearProductBrands")]
    public IActionResult ClearProductBrands(Product product)
    {
        return _productsService.ClearProductBrands(product);
    }
    
    [HttpGet("GetProductPhotos")]
    public IActionResult GetProductPhotos(Product product)
    {
        return _productsService.GetProductPhotos(product);
    }

    [HttpPost("AddProductPhoto")]
    public IActionResult AddProductPhoto(ProductPhoto productPhoto)
    {
        return _productsService.AddProductPhoto(productPhoto);
    }

    [HttpPost("DeleteProductPhoto")]
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
    public IActionResult AddProductPhotos(List<ProductPhoto> productPhotos)
    {
        return _productsService.AddProductPhotos(productPhotos);
    }
    
    [HttpPost("SetProductPhotos")]
    public IActionResult SetProductPhotos(List<ProductPhoto> productPhotos)
    {
        return _productsService.SetProductPhotos(productPhotos);
    }

    [HttpPost("ClearProductPhotos")]
    public IActionResult ClearProductPhotos(Product product)
    {
        return _productsService.ClearProductPhotos(product);
    }
    
    [HttpPost("AddNewProductPrice")]
    public IActionResult AddNewProductPrice(ProductPrice productPrice)
    {
        return _productsService.AddNewProductPrice(productPrice);
    }
    
    
    [HttpPost("DeleteProductPrice")]
    public IActionResult DeleteProductPrice(ProductPrice productPrice)
    {
        return _productsService.DeleteProductPrice(productPrice);
    }
}