using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Api.Models.Product;

public class Product
{
    public Product(int id, string? name, decimal? currentPrice, Category category, decimal? sale, List<ProductPrice> productPrices, List<Category> productCategories, List<ProductPhoto> productPhotos, List<Brand> productBrands)
    {
        Id = id;
        Name = name;
        CurrentPrice = currentPrice;
        Category = category;
        Sale = sale;
        ProductPrices = productPrices;
        ProductCategories = productCategories;
        ProductPhotos = productPhotos;
        ProductBrands = productBrands;
    }

    public Product()
    {
        Category = new Category();
        ProductPrices = new List<ProductPrice>();
        ProductCategories = new List<Category>();
        ProductPhotos = new List<ProductPhoto>();
        ProductBrands = new List<Brand>();
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Product name is required")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Price is required")]
    [Range(1, Double.MaxValue, ErrorMessage = "Price cannot be equals or between 0")]
    public decimal?  CurrentPrice { get; set; }
    [Required(ErrorMessage = "Category id required")]
    public Category Category { get; set; }
    public decimal? Sale { get; set; } = 0;
    public List<ProductPrice> ProductPrices { get; set; }
    public List<Category> ProductCategories { get; set; }
    public List<ProductPhoto> ProductPhotos { get; set; }
    public List<Brand> ProductBrands { get; set; }
}