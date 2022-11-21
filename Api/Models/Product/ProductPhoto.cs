namespace Api.Models.Product;

public class ProductPhoto
{
    public ProductPhoto()
    {
        Product = new Product();
    }

    public ProductPhoto(int id, Product product, string photoPath)
    {
        Id = id;
        PhotoPath = photoPath;
        Product = new Product();
    }

    public int Id { get; set; }
    public Product Product { get; set; }
    public string PhotoPath { get; set; }
}