using System.ComponentModel.DataAnnotations;

namespace Api.Models.Product;

public class Brand
{
    public Brand()
    {
        
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Brand name is required")]
    public string Name { get; set;}
    public string? PhotoUrl { get; set; }
}