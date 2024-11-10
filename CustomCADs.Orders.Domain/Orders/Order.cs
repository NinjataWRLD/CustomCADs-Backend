using CustomCADs.Orders.Domain.Orders.Enum;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Orders.Domain.Orders;

public class Order : BaseAggregateRoot
{
    private Order() { }    
    private Order(string name, string description, Purpose purpose, OrderStatus status, string imagePath, Guid buyerId) : this()
    {
        Name = name;
        Description = description;
        Purpose = purpose;
        OrderStatus = status;
        ImagePath = imagePath;
        BuyerId = buyerId;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public Purpose Purpose { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public Guid BuyerId { get; set; }
    public Guid? DesignerId { get; set; }

    public static Order Create(string name, string description, Purpose purpose, string imagePath, Guid buyerId)
    {
        return new(name, description, purpose, OrderStatus.Pending, imagePath, buyerId);
    }
}
