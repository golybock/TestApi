namespace Api.Models.Product.Brand;

public class Brand
{
    public Brand(int id, string name, string description, string photoUrl)
    {
        Id = id;
        Name = name;
        Description = description;
        PhotoUrl = photoUrl;
    }

    public Brand()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
}