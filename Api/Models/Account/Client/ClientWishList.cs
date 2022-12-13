namespace Api.Models.Client.CLient;

public class ClientWishList
{
    public ClientWishList(int id, int clientId, int productId, DateTime dateTimeAdded)
    {
        Id = id;
        ClientId = clientId;
        ProductId = productId;
        DateTimeAdded = dateTimeAdded;
    }

    public ClientWishList()
    {
    }

    public int Id { get; set; }
    public int ClientId { get; set; }
    public int ProductId { get; set; }
    public DateTime DateTimeAdded { get; set; }
}