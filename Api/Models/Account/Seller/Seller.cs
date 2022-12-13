namespace Api.Models.Account.Seller;

public class Seller : Account
{
    public Seller(int id, string login, string password, string token, DateTime dateTimeOfRegistration, AccountData data, List<AccountPhoto> accountPhotos, int inn, string description, List<SellerProduct> sellerProducts) : base(id, login, password, token, dateTimeOfRegistration, data, accountPhotos)
    {
        Inn = inn;
        Description = description;
        SellerProducts = sellerProducts;
    }

    public Seller(int inn, string description, List<SellerProduct> sellerProducts)
    {
        Inn = inn;
        Description = description;
        SellerProducts = sellerProducts;
    }

    public Seller()
    {
        SellerProducts = new List<SellerProduct>();
    }

    public int Inn { get; set; }
    public string Description { get; set; }
    public List<SellerProduct> SellerProducts { get; set; }
}