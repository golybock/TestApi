namespace Api.Models.Account.Seller;

public class ProductPrice
{
    public int Id { get; set; }
    public int SellerProductId { get; set; }
    public decimal Price { get; set; }
    public int SaleLevel { get; set; }
    public DateTime DateTimeOfSet { get; set; }
    public DateTime DateTimeOfStart { get; set; }
    public DateTime DateTimeOfEnd { get; set; }
}