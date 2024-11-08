using CustomCADs.Shared.Core.Entities;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Orders.Domain.OrderCads;

public class OrderCad : IEntity
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Cad Cad { get; set; } = new();
}
