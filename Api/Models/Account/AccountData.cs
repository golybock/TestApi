namespace Api.Models.Account;

public class AccountData
{
    public AccountData(int accountId, string phoneNumber, string email, string firstName, string lastName, string middleName)
    {
        AccountId = accountId;
        PhoneNumber = phoneNumber;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    public AccountData()
    {
    }

    public int AccountId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
}