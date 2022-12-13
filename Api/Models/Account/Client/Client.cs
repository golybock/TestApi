using Api.Models.Account;

namespace Api.Models.Client.CLient;

public class Client : Account.Account
{
    public Client(int id, string login, string password, string token, DateTime dateTimeOfRegistration, AccountData data, List<AccountPhoto> accountPhotos, int totalDiscount, decimal balance, List<ClientWishList> clientWishList) : base(id, login, password, token, dateTimeOfRegistration, data, accountPhotos)
    {
        TotalDiscount = totalDiscount;
        Balance = balance;
        ClientWishList = clientWishList;
    }

    public Client(int totalDiscount, decimal balance, List<ClientWishList> clientWishList)
    {
        TotalDiscount = totalDiscount;
        Balance = balance;
        ClientWishList = clientWishList;
    }

    public Client()
    {
        ClientWishList = new List<ClientWishList>();
    }
    public int TotalDiscount { get; set; }
    public decimal Balance { get; set; }
    public List<ClientWishList> ClientWishList { get; set; }
}