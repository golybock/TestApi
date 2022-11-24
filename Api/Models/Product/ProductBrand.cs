namespace Api.Models.Product;

public class ProductBrand
{
    public ProductBrand()
    {

    }

    public int Id { get; set; }
    public int ProductId { get; set; }
    public int BrandId { get; set; }
    
}