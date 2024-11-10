using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Orders.Domain.OrderCads;

public class OrderCad : BaseAggregateRoot
{
    private OrderCad() { }
    private OrderCad(Guid orderId, Cad cad) : this()
    {
        OrderId = orderId;
        Cad = cad;
    }

    public Guid Id { get; set; }
    public Cad Cad { get; set; } = new();
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public static OrderCad Create(Guid orderId, Cad cad)
    {
        return new(orderId, cad);
    }
}
