using System.ComponentModel.DataAnnotations;

namespace Api.Models.Order;

public class OrderProducts
{
    public OrderProducts()
    {
        Order = new Order();
        Product = new Product.Product();
    }

    public OrderProducts(int id, Order order, Product.Product product, int count, decimal priceForOne)
    {
        Id = id;
        Order = order;
        Product = product;
        Count = count;
        PriceForOne = priceForOne;
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Order is required")]
    public Order Order { get; set; }
    [Required(ErrorMessage = "Product is required")]
    public Product.Product Product { get; set; }
    public int Count { get; set; } = 1;
    public decimal PriceForOne { get; set; } = 1;
}