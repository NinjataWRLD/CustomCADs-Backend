using CustomCADs.Orders.Domain.Orders.Enum;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.Orders;

public class Order : BaseAggregateRoot
{
    private Order() { }
    private Order(string name, string description, Delivery delivery, UserId buyerId) : this()
    {
        Name = name;
        Description = description;
        Delivery = delivery;
        OrderStatus = OrderStatus.Pending;
        OrderDate = DateTime.UtcNow;
        BuyerId = buyerId;
    }

    public OrderId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public Image Image { get; set; } = new();
    public Delivery Delivery { get; set; } = Delivery.CreateNone();
    public UserId BuyerId { get; set; }
    public UserId? DesignerId { get; set; }

    public static Order Create(string name, string description, Delivery delivery, UserId buyerId)
        => new Order(name, description, delivery, buyerId)
            .ValidateName()
            .ValidateDescription();
}
