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

    public IActionResult GetProductBrands(Product product)
    {
        return _productRepository.GetProductBrands(product);
    }

    public IActionResult GetBrands()
    {
        return _productRepository.GetBrands();
    }

    public IActionResult AddBrandToProduct(Product product, Brand brand)
    {
        return _productRepository.AddBrandToProduct(product, brand);
    }

    public IActionResult AddBrandsToProduct(Product product, List<Brand> brands)
    {
        return _productRepository.AddBrandsToProduct(product, brands);
    }

    public IActionResult DeleteBrandFromProduct(Product product, Brand brand)
    {
        return _productRepository.DeleteBrandFromProduct(product, brand);
    }

    public IActionResult SetProductBrands(Product product, List<Brand> brands)
    {
        return _productRepository.SetProductBrands(product, brands);
    }

    public IActionResult ClearProductBrands(Product product)
    {
        return _productRepository.ClearProductBrands(product);
    }

    public IActionResult AddProductPhoto(Product product, ProductPhoto productPhoto)
    {
        return _productRepository.AddProductPhoto(product, productPhoto);
    }

    public IActionResult DeleteProductPhoto(ProductPhoto productPhoto)
    {
        return _productRepository.DeleteProductPhoto(productPhoto);
    }

    public IActionResult EditProductPhoto(ProductPhoto productPhoto)
    {
        return _productRepository.EditProductPhoto(productPhoto);
    }

    public IActionResult AddProductPhotos(Product product, List<ProductPhoto> productPhotos)
    {
        return _productRepository.AddProductPhotos(product, productPhotos);
    }

    public IActionResult SetProductPhotos(Product product, List<ProductPhoto> productPhotos)
    {
        return _productRepository.SetProductPhotos(product, productPhotos);
    }

    public IActionResult ClearProductPhotos(Product product)
    {
        return _productRepository.ClearProductPhotos(product);
    }

    public IActionResult AddNewProductPrice(Product product, ProductPrice productPrice)
    {
        return _productRepository.AddNewProductPrice(product, productPrice);
    }

    public IActionResult DeleteProductPrice(ProductPrice productPrice)
    {
        return _productRepository.DeleteProductPrice(productPrice);
    }
}