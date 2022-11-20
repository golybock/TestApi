using System.ComponentModel.DataAnnotations;

namespace Api.Models.Order;

public class Order
{
    public Order()
    {
        Client = new Client.Client();
        DateTimeOfCreation = new DateTime();
    }

    public Order(int id, Client.Client client, DateTime dateTimeOfCreation, decimal totalCost)
    {
        Id = id;
        Client = client;
        DateTimeOfCreation = dateTimeOfCreation;
        TotalCost = totalCost;
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Client is required")]
    public Client.Client Client { get; set; }
    [Required(ErrorMessage = "DatetimeOfCreation is required")]
    public DateTime DateTimeOfCreation { get; set; }

    public decimal TotalCost { get; set; } = 0;
}