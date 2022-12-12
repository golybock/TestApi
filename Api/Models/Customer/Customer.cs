using System.ComponentModel.DataAnnotations;

namespace Api.Models.Customer;

public class Customer
{
    public Customer(int id, string name, string photoUrl)
    {
        Id = id;
        Name = name;
        PhotoUrl = photoUrl;
    }

    public Customer()
    {
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Customer name is required")]
    public string Name { get; set; }
    public string? PhotoUrl { get; set; }
}