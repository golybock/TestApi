using System.ComponentModel.DataAnnotations;

namespace Api.Models.Client;

public class Client
{
    public Client()
    {
        DateTimeOfRegistration = new DateTime();
    }
    
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Token is required")]
    public string Token { get; set; }
    public string? Password { get; set; }
    [Required(ErrorMessage = "DateTimeOfRegistration is required")]
    public DateTime? DateTimeOfRegistration { get; set; }
}