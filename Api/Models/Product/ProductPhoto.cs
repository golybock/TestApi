namespace Api.Models.Product;

public class ProductPhoto
{
    public ProductPhoto()
    {
        
    }

    public ProductPhoto(int id, Product product, string photoPath)
    {
        Id = id;
        PhotoPath = photoPath;
    }

    public int Id { get; set; }
    public string PhotoPath { get; set; }
}