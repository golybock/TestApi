namespace Api.Models.Account;

public class AccountPhoto
{
    public AccountPhoto(int id, int accountId, string photoUrl, DateTime dateTimeOfSet)
    {
        Id = id;
        AccountId = accountId;
        PhotoUrl = photoUrl;
        DateTimeOfSet = dateTimeOfSet;
    }

    public AccountPhoto()
    {
    }

    public int Id { get; set; }
    [field: NonSerialized]
    public int AccountId { get; set; }
    public string PhotoUrl { get; set; }
    public DateTime DateTimeOfSet { get; set; }
}