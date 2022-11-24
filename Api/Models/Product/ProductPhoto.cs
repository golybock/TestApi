namespace Api.Models.Product;

public class ProductPhoto
{
    public ProductPhoto()
    {
        
    }

    public int Id { get; set; }
    public int ProductId { get; set; }
    public string PhotoPath { get; set; }
}