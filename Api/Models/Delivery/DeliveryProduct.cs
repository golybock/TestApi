using Api.Models.Account.Seller;

namespace Api.Models.Delivery;

public class DeliveryProduct
{
    public DeliveryProduct(int id, SellerProduct sellerProduct, int deliveryId, decimal price, int count)
    {
        Id = id;
        SellerProduct = sellerProduct;
        DeliveryId = deliveryId;
        Price = price;
        Count = count;
    }

    public DeliveryProduct()
    {
        SellerProduct = new SellerProduct();
    }

    public int Id { get; set; }
    public SellerProduct SellerProduct { get; set; }
    public int DeliveryId { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }
}