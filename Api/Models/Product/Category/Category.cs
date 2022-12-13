namespace Api.Models.Product.Category;

public class Category
{
    public Category(int id, string name, string description, DateTime dateTimeOfCreation, bool isActive, int parentCategoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        DateTimeOfCreation = dateTimeOfCreation;
        IsActive = isActive;
        ParentCategoryId = parentCategoryId;
    }

    public Category()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [field: NonSerialized]
    public DateTime DateTimeOfCreation { get; set; }
    public bool IsActive { get; set; }
    public int ParentCategoryId { get; set; }
    
}