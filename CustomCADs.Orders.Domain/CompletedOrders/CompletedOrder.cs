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
    private CompletedOrder(string name, string description, bool delivery, DateTime orderDate, AccountId buyerId, AccountId designerId, CadId cadId) : this()
    {
        Name = name;
        Description = description;
        Delivery = delivery;
        PurchaseDate = DateTime.UtcNow;
        OrderDate = orderDate;
        BuyerId = buyerId;
        DesignerId = designerId;
        CadId = cadId;
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
        bool delivery,
        DateTime orderDate,
        AccountId buyerId,
        AccountId designerId,
        CadId cadId
    ) => new CompletedOrder(name, description, delivery, orderDate, buyerId, designerId, cadId)
            .ValidateName()
            .ValidateDescription()
            .ValidateOrderDate();

    public static CompletedOrder CreateWithId(
        CompletedOrderId id,
        string name,
        string description,
        bool delivery,
        DateTime orderDate,
        AccountId buyerId,
        AccountId designerId,
        CadId cadId
    ) => new CompletedOrder(name, description, delivery, orderDate, buyerId, designerId, cadId)
    {
        Id = id
    }
    .ValidateName()
    .ValidateDescription()
    .ValidateOrderDate();

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
