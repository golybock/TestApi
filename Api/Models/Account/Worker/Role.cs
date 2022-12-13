namespace Api.Models.Account.Worker;

public class Role
{
    public Role(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public Role()
    {
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}