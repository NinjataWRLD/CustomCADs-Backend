using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Enums;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Orders.Domain.OrderDeliveries;

public class OrderDelivery : IAggregateRoot
{
    private OrderDelivery() { }    
    private OrderDelivery(Guid orderId, DeliveryStatus status) : this()
    {
        Status = status;
        OrderId = orderId;
    }

    public Guid Id { get; set; }
    public DeliveryStatus Status { get; set; }
    public Address Address { get; set; } = new();
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public static OrderDelivery Create(Guid orderId, DeliveryStatus status)
    {
        return new(orderId, status);
    }
}
