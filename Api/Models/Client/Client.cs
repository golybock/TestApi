using System.ComponentModel.DataAnnotations;

namespace Api.Models.Client;

public class Client
{
    public Client()
    {
        DateTimeOfRegistration = new DateTime();
    }

    public Client(int id, string email, string phoneNumber, string firstName, string lastName, string token, string password, DateTime dateTimeOfRegistration)
    {
        Id = id;
        Email = email;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        Token = token;
        Password = password;
        DateTimeOfRegistration = dateTimeOfRegistration;
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