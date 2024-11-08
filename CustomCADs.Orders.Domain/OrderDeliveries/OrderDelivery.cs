using CustomCADs.Orders.Domain.OrderDeliveries.Enums;
using CustomCADs.Orders.Domain.OrderDeliveries.ValueObjects;

namespace CustomCADs.Orders.Domain.OrderDeliveries;

public class OrderDelivery
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public DeliveryStatus Status { get; set; }
    public Address Address { get; set; } = new();
}
