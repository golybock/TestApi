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
        return _productRepository.GetProducts();
    }

    public IActionResult GetProductById(int id)
    {
        return _productRepository.GetProductById(id);
    }

    public IActionResult DeleteProduct(int id)
    {
        return _productRepository.DeleteProduct(id);
    }

    public IActionResult DeleteProduct(Product product)
    {
        return _productRepository.DeleteProduct(product);
    }

    public IActionResult AddProduct(Product product)
    {
        return _productRepository.AddProduct(product);
    }

    public IActionResult UpdateProduct(Product product)
    {
        return _productRepository.UpdateProduct(product);
    }

    public IActionResult GetCategory(int id)
    {
        return _productRepository.GetCategory(id);
    }

    public IActionResult GetCategories()
    {
        return _productRepository.GetCategories();
    }

    public IActionResult AddProductCategory(Category category)
    {
        return _productRepository.AddProductCategory(category);
    }

    public IActionResult EditProductCategory(Category category)
    {
        return _productRepository.EditProductCategory(category);
    }

    public IActionResult DeleteProductCategory(Category category)
    {
        return _productRepository.DeleteProductCategory(category);
    }

    public IActionResult AddBrand(Brand brand)
    {
        return _productRepository.AddBrand(brand);
    }

    public IActionResult DeleteBrand(Brand brand)
    {
        return _productRepository.DeleteBrand(brand);
    }

    public IActionResult EditBrand(Brand brand)
    {
        return _productRepository.EditBrand(brand);
    }

    public IActionResult GetProductBrands(int id)
    {
        return _productRepository.GetProductBrands(id);
    }

    public IActionResult GetBrands()
    {
        return _productRepository.GetBrands();
    }

    public IActionResult AddBrandToProduct(ProductBrand productBrand)
    {
        return _productRepository.AddBrandToProduct(productBrand);
    }

    public IActionResult AddBrandsToProduct(List<ProductBrand> productBrands)
    {
        return _productRepository.AddBrandsToProduct(productBrands);
    }

    public IActionResult DeleteBrandFromProduct(ProductBrand productBrand)
    {
        return _productRepository.DeleteBrandFromProduct(productBrand);
    }

    public IActionResult SetProductBrands(List<ProductBrand> productBrands)
    {
        return _productRepository.SetProductBrands(productBrands);
    }

    public IActionResult ClearProductBrands(Product product)
    {
        return _productRepository.ClearProductBrands(product);
    }

    public IActionResult GetProductPhotos(int id)
    {
        return _productRepository.GetProductPhotos(id);
    }

    public IActionResult AddProductPhoto(ProductPhoto productPhoto)
    {
        return _productRepository.AddProductPhoto(productPhoto);
    }

    public IActionResult DeleteProductPhoto(ProductPhoto productPhoto)
    {
        return _productRepository.DeleteProductPhoto(productPhoto);
    }

    public IActionResult EditProductPhoto(ProductPhoto productPhoto)
    {
        return _productRepository.EditProductPhoto(productPhoto);
    }

    public IActionResult AddProductPhotos(List<ProductPhoto> productPhotos)
    {
        return _productRepository.AddProductPhotos(productPhotos);
    }

    public IActionResult SetProductPhotos(List<ProductPhoto> productPhotos)
    {
        return _productRepository.SetProductPhotos(productPhotos);
    }
    
    public IActionResult ClearProductPhotos(Product product)
    {
        return _productRepository.ClearProductPhotos(product);
    }

    public IActionResult AddNewProductPrice(ProductPrice productPrice)
    {
        return _productRepository.AddNewProductPrice(productPrice);
    }
    
    public IActionResult DeleteProductPrice(ProductPrice productPrice)
    {
        return _productRepository.DeleteProductPrice(productPrice);
    }
}