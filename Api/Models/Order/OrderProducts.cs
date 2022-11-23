using System.ComponentModel.DataAnnotations;

namespace Api.Models.Order;

public class OrderProducts
{
    public OrderProducts()
    {
        Product = new Product.Product();
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Order is required")]
    public int OrderId { get; set; }
    [Required(ErrorMessage = "Product is required")]
    public Product.Product Product { get; set; }
    public int Count { get; set; } = 1;
    public decimal PriceForOne { get; set; } = 1;
}