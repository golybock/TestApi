using System.ComponentModel.DataAnnotations;

namespace Api.Models.Order;

public class Order
{
    public Order()
    {
        DateTimeOfCreation = new DateTime();
        OrderProductsList = new List<OrderProduct>();
        OrderStatusesList = new List<OrderStatuses>();
    }
    
    public int Id { get; set; }
    [Required(ErrorMessage = "Client is required")]
    public int ClientId { get; set; }
    [Required(ErrorMessage = "DatetimeOfCreation is required")]
    public DateTime DateTimeOfCreation { get; set; }
    public decimal TotalCost { get; set; } = 0;
    public List<OrderProduct> OrderProductsList { get; set; }
    public List<OrderStatuses> OrderStatusesList { get; set; }

    public void Addproduct(OrderProduct orderProducts)
    {
        OrderProductsList.Add(orderProducts);
    }

    public void AddOrderStatuses(OrderStatuses orderStatuses)
    {
        OrderStatusesList.Add(orderStatuses);
    }
}