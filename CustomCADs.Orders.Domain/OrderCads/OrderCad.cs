using CustomCADs.Orders.Domain.OrderCads.ValueObjects;
using CustomCADs.Shared.Core.Entities;

namespace CustomCADs.Orders.Domain.OrderCads;

public class OrderCad : IEntity
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Cad Cad { get; set; } = new();
}
