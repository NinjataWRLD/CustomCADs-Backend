﻿using CustomCADs.Cart.Domain.Cart.Entities;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Enums;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Cart.Domain.ItemDeliveries;

public class ItemDelivery : IAggregateRoot
{
    private ItemDelivery() { }
    private ItemDelivery(DeliveryStatus status, Address address, Guid itemId) : this()
    {
        Status = status;
        Address = address;
        ItemId = itemId;
    }

    public Guid Id { get; set; }
    public DeliveryStatus Status { get; set; }
    public Address Address { get; set; } = new();
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public static ItemDelivery Create(DeliveryStatus status, Address addresses, Guid itemId)
    {
        return new(status, addresses, itemId);
    }
}
