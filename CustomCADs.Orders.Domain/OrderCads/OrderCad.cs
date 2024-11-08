using CustomCADs.Orders.Domain.OrderCads.ValueObjects;

namespace CustomCADs.Orders.Domain.OrderCads;

public class OrderCad
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Cad Cad { get; set; } = new();
}
