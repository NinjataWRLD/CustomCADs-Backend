using CustomCADs.Shared.Core.Entities;
using CustomCADs.Shared.Core.Enums;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Orders.Domain.OrderDeliveries;

public class OrderDelivery : IEntity
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public DeliveryStatus Status { get; set; }
    public Address Address { get; set; } = new();
}
