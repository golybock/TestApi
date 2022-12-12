using Api.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services.ServiceInterfaces;

public interface IProductsService
{
    // product
    public IActionResult GetProducts();
    public IActionResult GetProductById(int productId);
    public IActionResult DeleteProduct(int productId);
    public IActionResult AddProduct(Product product);
    public IActionResult UpdateProduct(Product product);
    // category
    public IActionResult GetCategory(int categoryId);
    public IActionResult GetCategories();
    public IActionResult AddProductCategory(Category category);
    public IActionResult EditProductCategory(Category category);
    public IActionResult DeleteProductCategory(int productCategoryId);
    // brand
    public IActionResult AddBrand(Brand brand);
    public IActionResult DeleteBrand(int brandId);
    public IActionResult EditBrand(Brand brand);
    // product brand
    public IActionResult GetProductBrands(int productBrandId);
    public IActionResult GetBrands();
    public IActionResult AddBrandToProduct(ProductBrand productBrand);
    public IActionResult AddBrandsToProduct(List<ProductBrand> productBrands);
    public IActionResult DeleteBrandFromProduct(int productBrandId);
    public IActionResult SetProductBrands(List<ProductBrand> productBrands);
    public IActionResult ClearProductBrands(int productId);
    // product photo
    public IActionResult GetProductPhotos(int id);
    public IActionResult AddProductPhoto(ProductPhoto productPhoto);
    public IActionResult DeleteProductPhoto(int productPhotoId);
    public IActionResult EditProductPhoto(ProductPhoto productPhoto);
    public IActionResult AddProductPhotos(List<ProductPhoto> productPhotos);
    public IActionResult SetProductPhotos(List<ProductPhoto> productPhotos);
    public IActionResult ClearProductPhotos(int productId);
    // product price
    public IActionResult AddNewProductPrice(ProductPrice productPrice);
    public IActionResult DeleteProductPrice(int productPriceId);
}