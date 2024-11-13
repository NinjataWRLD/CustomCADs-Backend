﻿using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders.Items;
using CustomCADs.Orders.Domain.GalleryOrders.Entities;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;

namespace CustomCADs.Orders.Domain.GalleryOrders;

public class GalleryOrder : BaseAggregateRoot
{
    private readonly List<GalleryOrderItem> items = [];

    private GalleryOrder() { }
    private GalleryOrder(DeliveryType deliveryType, UserId buyerId) : this()
    {
        DeliveryType = deliveryType;
        BuyerId = buyerId;
        PurchaseDate = DateTime.UtcNow;
        Total = Items.Sum(i => i.Price.Amount);
    }

    public GalleryOrderId Id { get; init; }
    public decimal Total { get; private set; }
    public DeliveryType DeliveryType { get; }
    public DateTime PurchaseDate { get; }
    public UserId BuyerId { get; private set; }
    public IReadOnlyCollection<GalleryOrderItem> Items => items.AsReadOnly();

    public static GalleryOrder CreateDigital(UserId buyerId)
        => new GalleryOrder(DeliveryType.Digital, buyerId)
            .ValidateItems();

    public static GalleryOrder CreatePhysical(UserId buyerId)
        => new GalleryOrder(DeliveryType.Physical, buyerId)
            .ValidateItems();

    public static GalleryOrder CreateDigitalAndPhysical(UserId buyerId)
        => new GalleryOrder(DeliveryType.Both, buyerId)
            .ValidateItems();

    public GalleryOrder AddItem(Money price, int quantity, ProductId productId)
    {
        items.Add(GalleryOrderItem.Create(price, quantity, productId, Id));
        return this;
    }

    public GalleryOrder RemoveItem(GalleryOrderItemId id)
    {
        items.Remove(items.FirstOrDefault(i => i.Id == id)
            ?? throw GalleryOrderItemNotFoundException.ById(id));
        return this;
    }
}
