using CustomCADs.Orders.Domain.Orders.Enum;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries;

namespace CustomCADs.Orders.Domain.Orders;

public class Order : BaseAggregateRoot
{
    private Order() { }
    private Order(string name, string description, Delivery delivery, Image image, Guid buyerId) : this()
    {
        Name = name;
        Description = description;
        Delivery = delivery;
        OrderStatus = OrderStatus.Pending;
        OrderDate = DateTime.UtcNow;
        Image = image;
        BuyerId = buyerId;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Image Image { get; set; } = new();
    public DateTime OrderDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public Delivery Delivery { get; set; } = Delivery.CreateNone();
    public Guid? DesignerId { get; set; }
    public Guid BuyerId { get; set; }

    public static Order Create(string name, string description, Delivery delivery, Image image, Guid buyerId)
    {
        return new(name, description, delivery, image, buyerId);
    }
}
