using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.Validation;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Orders.Domain.Orders;

public class Order : BaseAggregateRoot
{
    private Order() { }
    private Order(string name, string description, DeliveryType deliveryType, AccountId buyerId, ShipmentId? shipmentId) : this()
    {
        Name = name;
        Description = description;
        DeliveryType = deliveryType;
        OrderDate = DateTime.UtcNow;
        OrderStatus = OrderStatus.Pending;
        BuyerId = buyerId;
        ShipmentId = shipmentId;
    }

    public OrderId Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime OrderDate { get; }
    public DeliveryType DeliveryType { get; }
    public OrderStatus OrderStatus { get; private set; }
    public AccountId BuyerId { get; private set; }
    public AccountId? DesignerId { get; private set; }
    public CadId? CadId { get; private set; }
    public ShipmentId? ShipmentId { get; private set; }

    public static Order CreateDigital(
        string name,
        string description,
        AccountId buyerId
    ) => new Order(name, description, DeliveryType.Digital, buyerId, shipmentId: null)
            .ValidateName()
            .ValidateDescription();

    public static Order CreatePhysical(
        string name,
        string description,
        AccountId buyerId,
        ShipmentId? shipmentId
    ) => new Order(name, description, DeliveryType.Physical, buyerId, shipmentId)
            .ValidateName()
            .ValidateDescription();

    public static Order CreateDigitalAndPhysical(
        string name,
        string description,
        AccountId buyerId,
        ShipmentId? shipmentId
    ) => new Order(name, description, DeliveryType.Both, buyerId, shipmentId)
            .ValidateName()
            .ValidateDescription();

    public Order SetName(string name)
    {
        Name = name;
        this.ValidateName();
        return this;
    }

    public Order SetDescription(string description)
    {
        Description = description;
        this.ValidateDescription();
        return this;
    }

    public Order EraseDesignerId()
    {
        DesignerId = null;
        return this;
    }

    public Order SetDesignerId(AccountId designerId)
    {
        DesignerId = designerId;
        return this;
    }

    public Order SetCadId(CadId cadId)
    {
        if (DeliveryType is not DeliveryType.Digital and not DeliveryType.Both)
        {
            throw OrderValidationException.CadIdOnNonDigitalDeliveryType();
        }

        if (OrderStatus is not OrderStatus.Finished)
        {
            throw OrderValidationException.CannotSetCadIdOnNonFinishedOrder();
        }

        CadId = cadId;

        return this;
    }

    public Order SetShipmentId(ShipmentId shipmentId)
    {
        if (DeliveryType is DeliveryType.Physical or DeliveryType.Both)
        {
            ShipmentId = shipmentId;
        }
        else throw OrderValidationException.ShipmentIdOnNonPhysicalDeliveryType();

        return this;
    }

    public Order SetPendingStatus()
    {
        var newStatus = OrderStatus.Pending;

        if (!(OrderStatus == OrderStatus.Accepted || OrderStatus == OrderStatus.Begun))
        {
            throw OrderValidationException.InvalidStatus(Id, OrderStatus, newStatus);
        }

        OrderStatus = newStatus;
        return this;
    }

    public Order SetAcceptedStatus()
    {
        var newStatus = OrderStatus.Accepted;

        if (OrderStatus != OrderStatus.Pending)
        {
            throw OrderValidationException.InvalidStatus(Id, OrderStatus, newStatus);
        }

        OrderStatus = newStatus;
        return this;
    }

    public Order SetBegunStatus()
    {
        var newStatus = OrderStatus.Begun;

        if (OrderStatus != OrderStatus.Accepted)
        {
            throw OrderValidationException.InvalidStatus(Id, OrderStatus, newStatus);
        }

        OrderStatus = newStatus;
        return this;
    }

    public Order SetFinishedStatus()
    {
        var newStatus = OrderStatus.Finished;

        if (OrderStatus != OrderStatus.Begun)
        {
            throw OrderValidationException.InvalidStatus(Id, OrderStatus, newStatus);
        }

        OrderStatus = newStatus;
        return this;
    }

    public Order SetReportedStatus()
    {
        var newStatus = OrderStatus.Reported;

        if (OrderStatus != OrderStatus.Pending)
        {
            throw OrderValidationException.InvalidStatus(Id, OrderStatus, newStatus);
        }

        OrderStatus = newStatus;
        return this;
    }

    public Order SetRemovedStatus()
    {
        var newStatus = OrderStatus.Removed;

        if (OrderStatus != OrderStatus.Reported)
        {
            throw OrderValidationException.InvalidStatus(Id, OrderStatus, newStatus);
        }

        OrderStatus = newStatus;
        return this;
    }
}
