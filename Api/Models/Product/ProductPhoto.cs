namespace Api.Models.Product;

public class ProductPhoto
{
    public ProductPhoto(int id, int productId, string photoUrl, bool isMain)
    {
        Id = id;
        ProductId = productId;
        PhotoUrl = photoUrl;
        IsMain = isMain;
    }

    public ProductPhoto()
    {
    }

    public int Id { get; set; }
    public int ProductId { get; set; }
    public string PhotoUrl { get; set; }
    public bool IsMain { get; set; }
}