using Api.Models.Account.Seller;
using Api.Models.Delivery.Status;

namespace Api.Models.Delivery;

public class Delivery
{
    public Delivery(int id, decimal cost, DateTime dateTimeOfCreation, DateTime dateTimeOfReceiving, List<DeliveryStatus> deliveryStatusList, List<SellerProduct> sellerProducts)
    {
        Id = id;
        Cost = cost;
        DateTimeOfCreation = dateTimeOfCreation;
        DateTimeOfReceiving = dateTimeOfReceiving;
        DeliveryStatusList = deliveryStatusList;
        SellerProducts = sellerProducts;
    }

    public Delivery()
    {
        DeliveryStatusList = new List<DeliveryStatus>();
    }

    public int Id { get; set; }
    public decimal Cost { get; set; }
    public DateTime DateTimeOfCreation { get; set; }
    public DateTime DateTimeOfReceiving { get; set; }
    public List<DeliveryStatus> DeliveryStatusList { get; set; }
    public List<SellerProduct> SellerProducts { get; set; }
}