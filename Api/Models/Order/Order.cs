using System.ComponentModel.DataAnnotations;

namespace Api.Models.Order;

public class Order
{
    public Order()
    {
        Client = new Client.Client();
        DateTimeOfCreation = new DateTime();
        OrderProductsList = new List<OrderProducts>();
        OrderStatusesList = new List<OrderStatuses>();
    }
    
    public int Id { get; set; }
    [Required(ErrorMessage = "Client is required")]
    public Client.Client Client { get; set; }
    [Required(ErrorMessage = "DatetimeOfCreation is required")]
    public DateTime DateTimeOfCreation { get; set; }

    public decimal TotalCost { get; set; } = 0;
    public List<OrderProducts> OrderProductsList { get; set; }
    public List<OrderStatuses> OrderStatusesList { get; set; }
}