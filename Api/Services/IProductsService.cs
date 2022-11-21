using Api.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IProductsService
{
    // product
    public IActionResult GetProducts();
    public IActionResult GetProductById(int id);
    public IActionResult DeleteProduct(int id);
    public IActionResult DeleteProduct(Product product);
    public IActionResult AddProduct(Product product);
    public IActionResult UpdateProduct(Product product);
    // category
    public IActionResult AddProductCategory(Category category);
    public IActionResult EditProductCategory(Category category);
    public IActionResult DeleteProductCategory(Category category);
    // brand
    public IActionResult AddBrand(Brand brand);
    public IActionResult DeleteBrand(Brand brand);
    public IActionResult EditBrand(Brand brand);
    // product brand
    public IActionResult GetProductBrands(Product product);
    public IActionResult GetBrands();
    public IActionResult AddBrandToProduct(ProductBrand productBrand);
    public IActionResult AddBrandsToProduct(List<ProductBrand> productBrands);
    public IActionResult DeleteBrandFromProduct(ProductBrand productBrand);
    public IActionResult SetProductBrands(List<ProductBrand> productBrands);
    public IActionResult ClearProductBrands(Product product);
    // product photo
    public IActionResult AddProductPhoto(ProductPhoto productPhoto);
    public IActionResult DeleteProductPhoto(ProductPhoto productPhoto);
    public IActionResult EditProductPhoto(ProductPhoto productPhoto);
    public IActionResult AddProductPhotos(List<ProductPhoto> productPhotos);
    public IActionResult SetProductPhotos(List<ProductPhoto> productPhotos);
    public IActionResult ClearProductPhotos(Product product);
    // product price
    public IActionResult AddNewProductPrice(ProductPrice productPrice);
    public IActionResult DeleteProductPrice(ProductPrice productPrice);
}