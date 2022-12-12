using Api.Database;
using Api.Models.Product;
using Api.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public class ProductsService : IProductsService
{
    private readonly ProductRepository _productRepository;
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

    public IActionResult DeleteProductCategory(int productCategoryId)
    {
        return _productRepository.DeleteProductCategory(productCategoryId);
    }

    public IActionResult AddBrand(Brand brand)
    {
        return _productRepository.AddBrand(brand);
    }

    public IActionResult DeleteBrand(int brandId)
    {
        return _productRepository.DeleteBrand(brandId);
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

    public IActionResult DeleteBrandFromProduct(int productBrandId)
    {
        return _productRepository.DeleteBrandFromProduct(productBrandId);
    }
    
    public IActionResult SetProductBrands(List<ProductBrand> productBrands)
    {
        return _productRepository.SetProductBrands(productBrands);
    }

    public IActionResult ClearProductBrands(int productId)
    {
        return _productRepository.ClearProductBrands(productId);
    }
    
    public IActionResult GetProductPhotos(int id)
    {
        return _productRepository.GetProductPhotos(id);
    }

    public IActionResult AddProductPhoto(ProductPhoto productPhoto)
    {
        return _productRepository.AddProductPhoto(productPhoto);
    }

    public IActionResult DeleteProductPhoto(int productPhotoId)
    {
        return _productRepository.DeleteProductPhoto(productPhotoId);
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

    public IActionResult ClearProductPhotos(int productId)
    {
        return _productRepository.ClearOrderProducts(productId);
    }

    public IActionResult AddNewProductPrice(ProductPrice productPrice)
    {
        return _productRepository.AddNewProductPrice(productPrice);
    }

    public IActionResult DeleteProductPrice(int productPriceId)
    {
        return _productRepository.DeleteProductPrice(productPriceId);
    }
}