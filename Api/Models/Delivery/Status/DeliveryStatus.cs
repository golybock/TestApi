namespace Api.Models.Delivery.Status;

public class DeliveryStatus
{
    public DeliveryStatus(int id, int statusId, int deliveryId, DateTime dateTimeOfSet)
    {
        Id = id;
        StatusId = statusId;
        DeliveryId = deliveryId;
        DateTimeOfSet = dateTimeOfSet;
    }

    public DeliveryStatus()
    {
    }

    public int Id { get; set; }
    public int StatusId { get; set; }
    public int DeliveryId { get; set; }
    public DateTime DateTimeOfSet { get; set; }
}