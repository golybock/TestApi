using Api.Models;
using Api.Models.Product;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
    
    [HttpPost("DeleteProduct")]
    public IActionResult DeleteProduct(int id)
    {
        return _productsService.DeleteProduct(id);
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
    public IActionResult DeleteProductCategory(int productCategoryId)
    {
        return _productsService.DeleteProductCategory(productCategoryId);
    }
    
    [HttpPost("AddBrand")]
    public IActionResult AddBrand(Brand brand)
    {
        return _productsService.AddBrand(brand);
    }
    
    [HttpPost("DeleteBrand")]
    public IActionResult DeleteBrand(int brandId)
    {
        return _productsService.DeleteBrand(brandId);
    }
    
    [HttpPost("EditBrand")]
    public IActionResult EditBrand(Brand brand)
    {
        return _productsService.EditBrand(brand);
    }
    
    [HttpGet("GetProductBrands")]
    public IActionResult GetProductBrands(int id)
    {
        return _productsService.GetProductBrands(id);
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
    public IActionResult DeleteBrandFromProduct(int productBrandId)
    {
        return _productsService.DeleteBrandFromProduct(productBrandId);
    }

    [HttpPost("SetProductBrands")]
    public IActionResult SetProductBrands(List<ProductBrand> productBrands)
    {
        return _productsService.SetProductBrands(productBrands);
    }
    
    [HttpPost("ClearProductBrands")]
    public IActionResult ClearProductBrands(int productId)
    {
        return _productsService.ClearProductBrands(productId);
    }
    
    [HttpGet("GetProductPhotos")]
    public IActionResult GetProductPhotos(int id)
    {
        return _productsService.GetProductPhotos(id);
    }

    [HttpPost("AddProductPhoto")]
    public IActionResult AddProductPhoto(ProductPhoto productPhoto)
    {
        return _productsService.AddProductPhoto(productPhoto);
    }

    [HttpPost("DeleteProductPhoto")]
    public IActionResult DeleteProductPhoto(int productPhotoId)
    {
        return _productsService.DeleteProductPhoto(productPhotoId);
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
    public IActionResult ClearProductPhotos(int productId)
    {
        return _productsService.ClearProductPhotos(productId);
    }
    
    [HttpPost("AddNewProductPrice")]
    public IActionResult AddNewProductPrice(ProductPrice productPrice)
    {
        return _productsService.AddNewProductPrice(productPrice);
    }
    
    
    [HttpPost("DeleteProductPrice")]
    public IActionResult DeleteProductPrice(int productPriceId)
    {
        return _productsService.DeleteProductPrice(productPriceId);
    }
}