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

    public List<Product> GetProducts()
    {
        return _productRepository.GetProducts();
    }

    IActionResult IProductsService.GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    IActionResult IProductsService.GetProducts()
    {
        throw new NotImplementedException();
    }

    public Product GetProductById(int id)
    {
        return _productRepository.GetProductById(id);
    }
    
    public IActionResult DeleteProduct(int id)
    {
        return _productRepository.DeleteProduct(id);
    }

    public IActionResult DeleteProduct(Product product)
    {
        throw new NotImplementedException();
    }

    IActionResult IProductsService.AddProduct(Product product)
    {
        return AddProduct(product);
    }

    IActionResult IProductsService.UpdateProduct(Product product)
    {
        return UpdateProduct(product);
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

    public IActionResult AddCategoryToProduct(Product product, Category category)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddCategoriesProduct(Product product, List<Category> categories)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteCategoryFromProduct(Product product, Category category)
    {
        throw new NotImplementedException();
    }

    public IActionResult GetProductCategories(Product product)
    {
        throw new NotImplementedException();
    }

    public IActionResult SetCategoriesToProduct(Product product, List<Category> categories)
    {
        throw new NotImplementedException();
    }

    public IActionResult ClearProductCategories(Product product)
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

    public IActionResult DeleteProductPhoto(Product product, ProductPhoto productPhoto)
    {
        throw new NotImplementedException();
    }

    public IActionResult EditProductPhoto(Product product, ProductPhoto productPhoto)
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

    public IActionResult DeleteProductPrice(Product product, ProductPrice productPrice)
    {
        throw new NotImplementedException();
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