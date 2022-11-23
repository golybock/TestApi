using System.ComponentModel.DataAnnotations;

namespace Api.Models.Order;

public class OrderStatuses
{
    public OrderStatuses()
    {
        OrderStatus = new OrderStatus();
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Order is required")]
    public int OrderId { get; set; }
    [Required(ErrorMessage = "OrderStatus is required")]
    public OrderStatus OrderStatus { get; set; }
}