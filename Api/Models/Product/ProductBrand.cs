namespace Api.Models.Product;

public class ProductBrand
{
    public ProductBrand()
    {
        Product = new Product();
        Brand = new Brand();
    }

    public int Id { get; set; }
    public Product Product { get; set; }
    public Brand Brand { get; set; }
    
}