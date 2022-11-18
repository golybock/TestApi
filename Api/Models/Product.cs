using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Product
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Price is required")]
    [Range(1, Double.MaxValue, ErrorMessage = "Price cannot be equals or between 0")]
    public decimal?  Price { get; set; }
    public string? PhotoUrl { get; set; }
    public int? CategoryId { get; set; }
        
}