using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Orders.Domain.OrderCads;

public class OrderCad : IAggregateRoot
{
    public Guid Id { get; set; }
    public Cad Cad { get; set; } = new();
    public Guid OrderId { get; set; }
    public required Order Order { get; set; }
}
