using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Orders.Domain.OngoingOrders;

public class OngoingOrder : BaseAggregateRoot
{
    private OngoingOrder() { }
    private OngoingOrder(string name, string description, bool delivery, AccountId buyerId) : this()
    {
        Name = name;
        Description = description;
        OrderDate = DateTime.UtcNow;
        OrderStatus = OngoingOrderStatus.Pending;
        BuyerId = buyerId;
        Delivery = delivery;
    }

    public OngoingOrderId Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal? Price { get; private set; }
    public bool Delivery { get; private set; }
    public DateTime OrderDate { get; }
    public OngoingOrderStatus OrderStatus { get; private set; }
    public AccountId BuyerId { get; private set; }
    public AccountId? DesignerId { get; private set; }
    public CadId? CadId { get; private set; }

    public static OngoingOrder Create(
        string name,
        string description,
        bool delivery,
        AccountId buyerId
    ) => new OngoingOrder(name, description, delivery, buyerId)
            .ValidateName()
            .ValidateDescription();

    public static OngoingOrder CreateWithId(
        OngoingOrderId id,
        string name,
        string description,
        bool delivery,
        AccountId buyerId
    ) => new OngoingOrder(name, description, delivery, buyerId)
    {
        Id = id
    }
    .ValidateName()
    .ValidateDescription();

    public OngoingOrder SetName(string name)
    {
        Name = name;
        this.ValidateName();
        return this;
    }

    public OngoingOrder SetDescription(string description)
    {
        Description = description;
        this.ValidateDescription();
        return this;
    }

    public OngoingOrder SetDelivery(bool value)
    {
        Delivery = value;

        return this;
    }

    public OngoingOrder SetPrice(decimal price)
    {
        Price = price;
        this.ValidatePrice();
        return this;
    }

    public OngoingOrder SetCadId(CadId cadId)
    {
        if (DesignerId is null)
        {
            throw CustomValidationException<OngoingOrder>.Custom($"Cannot set a CadId on an Ongoing Order with no DesignerId");
        }
        CadId = cadId;

        return this;
    }

    public OngoingOrder SetDesignerId(AccountId designerId)
    {
        DesignerId = designerId;
        return this;
    }

    public OngoingOrder EraseDesignerId()
    {
        DesignerId = null;
        return this;
    }

    public OngoingOrder SetPendingStatus()
    {
        var newStatus = OngoingOrderStatus.Pending;

        if (OrderStatus is not (OngoingOrderStatus.Accepted or OngoingOrderStatus.Begun or OngoingOrderStatus.Reported))
        {
            throw CustomValidationException<OngoingOrder>.Status(newStatus, OrderStatus);
        }

        OrderStatus = newStatus;
        return this;
    }

    public OngoingOrder SetAcceptedStatus()
    {
        var newStatus = OngoingOrderStatus.Accepted;

        if (OrderStatus is not OngoingOrderStatus.Pending)
        {
            throw CustomValidationException<OngoingOrder>.Status(newStatus, OrderStatus);
        }

        OrderStatus = newStatus;
        return this;
    }

    public OngoingOrder SetBegunStatus()
    {
        var newStatus = OngoingOrderStatus.Begun;

        if (OrderStatus is not OngoingOrderStatus.Accepted)
        {
            throw CustomValidationException<OngoingOrder>.Status(newStatus, OrderStatus);
        }

        OrderStatus = newStatus;
        return this;
    }

    public OngoingOrder SetFinishedStatus()
    {
        var newStatus = OngoingOrderStatus.Finished;

        if (OrderStatus is not (OngoingOrderStatus.Accepted or OngoingOrderStatus.Begun))
        {
            throw CustomValidationException<OngoingOrder>.Status(newStatus, OrderStatus);
        }

        OrderStatus = newStatus;
        return this;
    }

    public OngoingOrder SetReportedStatus()
    {
        var newStatus = OngoingOrderStatus.Reported;

        if (OrderStatus is not (OngoingOrderStatus.Pending or OngoingOrderStatus.Accepted or OngoingOrderStatus.Begun or OngoingOrderStatus.Finished))
        {
            throw CustomValidationException<OngoingOrder>.Status(newStatus, OrderStatus);
        }

        OrderStatus = newStatus;
        return this;
    }

    public OngoingOrder SetRemovedStatus()
    {
        var newStatus = OngoingOrderStatus.Removed;

        if (OrderStatus is not OngoingOrderStatus.Reported)
        {
            throw CustomValidationException<OngoingOrder>.Status(newStatus, OrderStatus);
        }

        OrderStatus = newStatus;
        return this;
    }
}
