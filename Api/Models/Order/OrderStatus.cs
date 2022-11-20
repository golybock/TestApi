using System.ComponentModel.DataAnnotations;

namespace Api.Models.Order;

public class OrderStatus
{
    public OrderStatus(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public OrderStatus()
    {
        
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Status name is required")]
    public string Name { get; set; }
    public string? Description { get; set; }
}