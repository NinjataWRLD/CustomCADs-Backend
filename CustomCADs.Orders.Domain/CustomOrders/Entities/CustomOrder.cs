﻿using CustomCADs.Catalog.Domain.Common.Exceptions;
using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;
using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Orders.Domain.CustomOrders.Validation;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.CustomOrders.Entities;

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

    public CustomOrderId Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime OrderDate { get; }
    public DeliveryType DeliveryType { get; }
    public CustomOrderStatus OrderStatus { get; private set; }
    public Image Image { get; private set; } = new();
    public UserId BuyerId { get; private set; }
    public UserId? DesignerId { get; private set; }

    public static CustomOrder CreateDigital(string name, string description, UserId buyerId)
        => new CustomOrder(name, description, DeliveryType.Digital, buyerId)
            .ValidateName()
            .ValidateDescription();

    public static CustomOrder CreatePhysical(string name, string description, UserId buyerId)
        => new CustomOrder(name, description, DeliveryType.Physical, buyerId)
            .ValidateName()
            .ValidateDescription();

    public static CustomOrder CreateDigitalAndPhysical(string name, string description, UserId buyerId)
        => new CustomOrder(name, description, DeliveryType.Both, buyerId)
            .ValidateName()
            .ValidateDescription();

    public CustomOrder SetName(string name)
    {
        Name = name;
        this.ValidateName();
        return this;
    }

    public CustomOrder SetDescription(string description)
    {
        Description = description;
        this.ValidateDescription();
        return this;
    }

    public CustomOrder SetImagePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw CustomOrderValidationException.NotNull("Path");
        }
        Image = Image with { Path = path };

        return this;
    }

    public CustomOrder SetDesignerId(UserId designerId)
    {
        DesignerId = designerId;
        return this;
    }

    public CustomOrder SetPendingStatus()
    {
        var newStatus = CustomOrderStatus.Pending;

        if (!(OrderStatus == CustomOrderStatus.Accepted || OrderStatus == CustomOrderStatus.Begun))
        {
            throw CustomOrderStatusException.ById(Id, newStatus.ToString());
        }

        OrderStatus = newStatus;
        return this;
    }

    public CustomOrder SetAcceptedStatus()
    {
        var newStatus = CustomOrderStatus.Accepted;

        if (OrderStatus != CustomOrderStatus.Pending)
        {
            throw CustomOrderStatusException.ById(Id, newStatus.ToString());
        }

        OrderStatus = newStatus;
        return this;
    }

    public CustomOrder SetBegunStatus()
    {
        var newStatus = CustomOrderStatus.Begun;

        if (OrderStatus != CustomOrderStatus.Accepted)
        {
            throw CustomOrderStatusException.ById(Id, newStatus.ToString());
        }

        OrderStatus = newStatus;
        return this;
    }

    public CustomOrder SetFinishedStatus()
    {
        var newStatus = CustomOrderStatus.Finished;

        if (OrderStatus != CustomOrderStatus.Begun)
        {
            throw CustomOrderStatusException.ById(Id, newStatus.ToString());
        }

        OrderStatus = newStatus;
        return this;
    }

    public CustomOrder SetReportedStatus()
    {
        OrderStatus = CustomOrderStatus.Reported;
        return this;
    }

    public CustomOrder SetRemovedStatus()
    {
        var newStatus = CustomOrderStatus.Removed;

        if (OrderStatus != CustomOrderStatus.Reported)
        {
            throw CustomOrderStatusException.ById(Id, newStatus.ToString());
        }

        OrderStatus = newStatus;
        return this;
    }
}