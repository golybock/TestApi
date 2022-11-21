using Api.DB;
using Api.Models;
using Api.Models.Product;
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


    public IActionResult GetProducts()
    {
        throw new NotImplementedException();
    }

    public IActionResult GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public IActionResult UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddProductCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public IActionResult EditProductCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteProductCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddBrand(Brand brand)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteBrand(Brand brand)
    {
        throw new NotImplementedException();
    }

    public IActionResult EditBrand(Brand brand)
    {
        throw new NotImplementedException();
    }

    public IActionResult GetProductBrands(Product product)
    {
        throw new NotImplementedException();
    }

    public IActionResult GetBrands()
    {
        throw new NotImplementedException();
    }

    public IActionResult AddBrandToProduct(Product product, Brand brand)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddBrandsToProduct(Product product, List<Brand> brands)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteBrandFromProduct(Product product, Brand brand)
    {
        throw new NotImplementedException();
    }

    public IActionResult SetProductBrands(Product product, List<Brand> brands)
    {
        throw new NotImplementedException();
    }

    public IActionResult ClearProductBrands(Product product)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddProductPhoto(Product product, ProductPhoto productPhoto)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteProductPhoto(ProductPhoto productPhoto)
    {
        throw new NotImplementedException();
    }

    public IActionResult EditProductPhoto(ProductPhoto productPhoto)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddProductPhotos(Product product, List<ProductPhoto> productPhotos)
    {
        throw new NotImplementedException();
    }

    public IActionResult SetProductPhotos(Product product, List<ProductPhoto> productPhotos)
    {
        throw new NotImplementedException();
    }

    public IActionResult ClearProductPhotos(Product product)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddNewProductPrice(Product product, ProductPrice productPrice)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteProductPrice(ProductPrice productPrice)
    {
        throw new NotImplementedException();
    }
}