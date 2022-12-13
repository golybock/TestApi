namespace Api.Models.Product.Color;

public class Colors
{
    public Colors(int id, string name, string description, string code)
    {
        Id = id;
        Name = name;
        Description = description;
        Code = code;
        
    }

    public Colors()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
}