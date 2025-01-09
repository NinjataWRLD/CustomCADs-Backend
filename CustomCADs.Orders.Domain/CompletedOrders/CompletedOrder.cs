using CustomCADs.Orders.Domain.Common.Exceptions.CompletedOrder;
using CustomCADs.Orders.Domain.CompletedOrders.Validation;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Orders.Domain.CompletedOrders;

public class CompletedOrder : BaseAggregateRoot
{
    private CompletedOrder() { }
    private CompletedOrder(string name, string description, bool delivery, AccountId buyerId) : this()
    {
        Name = name;
        Description = description;
        OrderDate = DateTime.UtcNow;
        BuyerId = buyerId;
        Delivery = delivery;
    }

    public CompletedOrderId Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool Delivery { get; private set; }
    public DateTime OrderDate { get; }
    public DateTime PurchaseDate { get; }
    public AccountId BuyerId { get; private set; }
    public AccountId DesignerId { get; private set; }
    public CadId CadId { get; private set; }
    public ShipmentId? ShipmentId { get; private set; }

    public static CompletedOrder Create(
        string name,
        string description,
        AccountId buyerId
    ) => new CompletedOrder(name, description, delivery: false, buyerId)
            .ValidateName()
            .ValidateDescription();

    public static CompletedOrder CreateWithDelivery(
        string name,
        string description,
        AccountId buyerId
    ) => new CompletedOrder(name, description, delivery: true, buyerId)
        .ValidateName()
        .ValidateDescription();

    public static CompletedOrder CreateWithId(
        CompletedOrderId id,
        string name,
        string description,
        AccountId buyerId
    ) => new CompletedOrder(name, description, delivery: false, buyerId)
    {
        Id = id
    }
    .ValidateName()
    .ValidateDescription();

    public static CompletedOrder CreateWithDeliveryAndId(
        CompletedOrderId id,
        string name,
        string description,
        AccountId buyerId
    ) => new CompletedOrder(name, description, delivery: true, buyerId)
    {
        Id = id
    }
    .ValidateName()
    .ValidateDescription();

    public CompletedOrder SetShipmentId(ShipmentId shipmentId)
    {
        if (!Delivery)
        {
            throw CompletedOrderValidationException.ShipmentIdOnNonDelivery();
        }
        ShipmentId = shipmentId;

        return this;
    }
}
