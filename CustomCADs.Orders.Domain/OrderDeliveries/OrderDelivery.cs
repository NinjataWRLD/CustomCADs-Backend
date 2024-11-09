using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Enums;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Orders.Domain.OrderDeliveries;

public class OrderDelivery : IAggregateRoot
{
    public Guid Id { get; set; }
    public DeliveryStatus Status { get; set; }
    public Address Address { get; set; } = new();
    public Guid OrderId { get; set; }
    public required Order Order { get; set; }
}
