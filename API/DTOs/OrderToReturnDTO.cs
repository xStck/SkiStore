using Core.Entities.OrderAggregate;

namespace API.DTOs;

public class OrderToReturnDTO
{
    public int id { get; set; }
    public string BuyerEmail { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public Address ShipToAddress { get; set; }
    public string DeliveryMethod { get; set; }
    public decimal ShippingPrice { get; set; }
    public IReadOnlyList<OrderItemDTO> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
}