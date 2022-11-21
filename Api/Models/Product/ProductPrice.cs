namespace Api.Models.Product;

public class ProductPrice
{
    public ProductPrice()
    {
        Customer = new Customer.Customer();
        DateTime = new DateTime();
        Product = new Product();
    }

    public ProductPrice(int id, decimal price, DateTime dateTime, Product product, Customer.Customer customer)
    {
        Id = id;
        Price = price;
        DateTime = dateTime;
        Customer = customer;
        Product = product;
    }

    public int Id { get; set; }
    public decimal Price { get; set; }
    public DateTime DateTime { get; set; }
    public Customer.Customer Customer { get; set; }
    public Product Product { get; set; }
}