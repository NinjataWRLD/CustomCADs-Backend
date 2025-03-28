﻿using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Orders.Domain.CompletedOrders;

public class CompletedOrder : BaseAggregateRoot
{
    private CompletedOrder() { }
    private CompletedOrder(string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId, AccountId designerId, CadId cadId) : this()
    {
        Name = name;
        Description = description;
        Price = price;
        Delivery = delivery;
        PurchasedAt = DateTimeOffset.UtcNow;
        OrderedAt = orderedAt;
        BuyerId = buyerId;
        DesignerId = designerId;
        CadId = cadId;
    }

    public CompletedOrderId Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public bool Delivery { get; private set; }
    public DateTimeOffset OrderedAt { get; }
    public DateTimeOffset PurchasedAt { get; }
    public AccountId BuyerId { get; private set; }
    public AccountId DesignerId { get; private set; }
    public CadId CadId { get; private set; }
    public ShipmentId? ShipmentId { get; private set; }
    public CustomizationId? CustomizationId { get; private set; }

    public static CompletedOrder Create(
        string name,
        string description,
        decimal price,
        bool delivery,
        DateTimeOffset orderedAt,
        AccountId buyerId,
        AccountId designerId,
        CadId cadId
    ) => new CompletedOrder(name, description, price, delivery, orderedAt, buyerId, designerId, cadId)
            .ValidateName()
            .ValidateDescription()
            .ValidatePrice()
            .ValidateOrderedAt();

    public static CompletedOrder CreateWithId(
        CompletedOrderId id,
        string name,
        string description,
        decimal price,
        bool delivery,
        DateTimeOffset orderedAt,
        AccountId buyerId,
        AccountId designerId,
        CadId cadId
    ) => new CompletedOrder(name, description, price, delivery, orderedAt, buyerId, designerId, cadId)
    {
        Id = id
    }
    .ValidateName()
    .ValidateDescription()
    .ValidatePrice()
    .ValidateOrderedAt();

    public CompletedOrder SetShipmentId(ShipmentId shipmentId)
    {
        if (!Delivery)
        {
            throw CustomValidationException<CompletedOrder>.Custom("Cannot set a ShipmentId on a Completed Order not for Delivery.");
        }
        ShipmentId = shipmentId;

        return this;
    }

    public CompletedOrder SetCustomizationId(CustomizationId customizationId)
    {
        if (!Delivery)
        {
            throw CustomValidationException<CompletedOrder>.Custom("Cannot set a CustomizationId on a Completed Order not for Delivery.");
        }
        CustomizationId = customizationId;

        return this;
    }
}
