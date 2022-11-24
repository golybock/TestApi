using System.ComponentModel.DataAnnotations;

namespace Api.Models.Order;

public class OrderStatus
{
    public OrderStatus()
    {
        
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Status name is required")]
    public string Name { get; set; }
    public string? Description { get; set; }
}