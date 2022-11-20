using System.ComponentModel.DataAnnotations;

namespace Api.Models.Product;

public class Brand
{
    public Brand(int id, string name, string photoUrl)
    {
        Id = id;
        Name = name;
        PhotoUrl = photoUrl;
    }

    public Brand()
    {
        
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Brand name is required")]
    public string Name { get; set;}
    public string? PhotoUrl { get; set; }
}