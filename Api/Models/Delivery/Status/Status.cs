namespace Api.Models.Delivery.Status;

public class Status
{
    public Status(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public Status()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}