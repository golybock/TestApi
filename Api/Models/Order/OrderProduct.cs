using System.ComponentModel.DataAnnotations;

namespace Api.Models.Order;

public class OrderProduct
{
    public OrderProduct()
    {

    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Order is required")]
    public int OrderId { get; set; }
    [Required(ErrorMessage = "Product is required")]
    public int ProductId { get; set; }
    public int Count { get; set; } = 1;
    public decimal PriceForOne { get; set; } = 1;
}