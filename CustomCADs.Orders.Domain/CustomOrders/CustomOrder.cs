using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.CustomOrders;

public class CustomOrder : BaseAggregateRoot
{
    private CustomOrder() { }
    private CustomOrder(string name, string description, DeliveryType deliveryType, UserId buyerId) : this()
    {
        Name = name;
        Description = description;
        DeliveryType = deliveryType;
        OrderStatus = CustomOrderStatus.Pending;
        OrderDate = DateTime.UtcNow;
        BuyerId = buyerId;
    }

    public CustomOrderId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public CustomOrderStatus OrderStatus { get; set; }
    public Image Image { get; set; } = new();
    public UserId BuyerId { get; set; }
    public UserId? DesignerId { get; set; }

    public static CustomOrder Create(string name, string description, DeliveryType deliveryType, UserId buyerId)
        => new CustomOrder(name, description, deliveryType, buyerId)
            .ValidateName()
            .ValidateDescription();
}
