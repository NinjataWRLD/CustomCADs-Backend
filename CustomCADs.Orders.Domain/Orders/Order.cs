using CustomCADs.Orders.Domain.Orders.Enum;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Orders.Domain.Orders;

public class Order : BaseAggregateRoot
{
    private Order() { }    
    private Order(string name, string description, Purpose purpose, Image image, Guid buyerId) : this()
    {
        Name = name;
        Description = description;
        Purpose = purpose;
        OrderStatus = OrderStatus.Pending;
        OrderDate = DateTime.UtcNow;
        Image = image;
        BuyerId = buyerId;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Image Image { get; set; } = new();
    public Purpose Purpose { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public Guid BuyerId { get; set; }
    public Guid? DesignerId { get; set; }

    public static Order Create(string name, string description, Purpose purpose, Image image, Guid buyerId)
    {
        return new(name, description, purpose, image, buyerId);
    }
}
