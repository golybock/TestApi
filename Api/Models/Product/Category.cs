using System.ComponentModel.DataAnnotations;

namespace Api.Models.Product;

public class Category
{
    public Category()
    {
        
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Category name is required")]
    public string Name { get; set; }
    public string? Description { get; set; }
}