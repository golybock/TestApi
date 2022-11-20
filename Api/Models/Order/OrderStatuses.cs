using System.ComponentModel.DataAnnotations;

namespace Api.Models.Order;

public class OrderStatuses
{
    public OrderStatuses()
    {
        Order = new Order();
        OrderStatus = new OrderStatus();
    }

    public OrderStatuses(int id, Order order, OrderStatus orderStatus)
    {
        Id = id;
        Order = order;
        OrderStatus = orderStatus;
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Order is required")]
    public Order Order { get; set; }
    [Required(ErrorMessage = "OrderStatus is required")]
    public OrderStatus OrderStatus { get; set; }
}