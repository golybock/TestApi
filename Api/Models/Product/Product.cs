using Api.Models.Account.Seller;

namespace Api.Models.Product;

public class Product
{
    public Product(int id, string name, string description, Brand.Brand brand, Category.Category category, List<Seller> sellers)
    {
        Id = id;
        Name = name;
        Description = description;
        Brand = brand;
        Category = category;
        Sellers = sellers;
    }

    public Product()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Brand.Brand Brand { get; set; }
    public Category.Category Category { get; set; }
    public List<Seller> Sellers { get; set; }
}