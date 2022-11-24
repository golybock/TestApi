namespace Api.Models.Product;

public class ProductPrice
{
    public ProductPrice()
    {
        DateTime = new DateTime();
    }

    public int Id { get; set; }
    public decimal Price { get; set; }
    public DateTime DateTime { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
}