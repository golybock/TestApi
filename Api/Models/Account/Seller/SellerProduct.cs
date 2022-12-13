namespace Api.Models.Account.Seller;

public class SellerProduct
{
    public SellerProduct(int id, Seller seller, Product.Product product, DateTime dateTime, int entityInStock, List<ProductPrice> productPrices)
    {
        Id = id;
        Seller = seller;
        Product = product;
        DateTime = dateTime;
        EntityInStock = entityInStock;
        ProductPrices = productPrices;
    }

    public SellerProduct()
    {
        ProductPrices = new List<ProductPrice>();
        Product = new Product.Product();
        Seller = new Seller();
    }

    public int Id { get; set; }
    public Seller Seller { get; set; }
    public Product.Product Product{ get; set; }
    public DateTime DateTime { get; set; }
    public int EntityInStock { get; set; }
    public List<ProductPrice> ProductPrices { get; set; }
}