using CustomCADs.Orders.Domain.OrderCads;
using CustomCADs.Orders.Domain.OrderDeliveries;
using CustomCADs.Orders.Domain.Orders.Enum;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Enums;

namespace CustomCADs.Orders.Domain.Orders;

public class Order : IAggregateRoot
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Purpose Purpose { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public string BuyerId { get; set; } = string.Empty;
    public string? DesignerId { get; set; }
}
