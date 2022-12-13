namespace Api.Models.Account;

public class Account
{
    public Account(int id, string login, string password, string token, DateTime dateTimeOfRegistration, AccountData data, List<AccountPhoto> accountPhotos)
    {
        Id = id;
        Login = login;
        Password = password;
        Token = token;
        DateTimeOfRegistration = dateTimeOfRegistration;
        Data = data;
        AccountPhotos = accountPhotos;
    }

    public Account()
    {
        Data = new AccountData();
        AccountPhotos = new List<AccountPhoto>();
    }

    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
    public DateTime DateTimeOfRegistration { get; set; }
    
    public AccountData Data { get; set; }
    
    public List<AccountPhoto> AccountPhotos { get; set; }
}