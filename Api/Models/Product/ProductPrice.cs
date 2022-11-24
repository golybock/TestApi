namespace Api.Models.Product;

public class ProductPrice
{
    public ProductPrice()
    {
        Customer = new Customer.Customer();
        DateTime = new DateTime();
        Product = new Product();
    }

    public int Id { get; set; }
    public decimal Price { get; set; }
    public DateTime DateTime { get; set; }
    public Customer.Customer Customer { get; set; }
    public Product Product { get; set; }
}