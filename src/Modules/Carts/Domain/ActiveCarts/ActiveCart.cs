﻿using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Validation;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.ActiveCarts;

public class ActiveCart : BaseAggregateRoot
{
    private readonly List<ActiveCartItem> items = [];

    private ActiveCart() { }
    private ActiveCart(AccountId buyerId) : this()
    {
        BuyerId = buyerId;
    }

    public ActiveCartId Id { get; init; }
    public AccountId BuyerId { get; private set; }
    public bool HasDelivery => items.Any(i => i.ForDelivery);
    public double TotalDeliveryWeight => Items.Where(i => i.ForDelivery).Sum(i => i.Weight);
    public int TotalDeliveryCount => Items.Where(i => i.ForDelivery).Count();
    public int TotalCount => Items.Count;
    public IReadOnlyCollection<ActiveCartItem> Items => items.AsReadOnly();

    public static ActiveCart Create(AccountId buyerId)
        => new ActiveCart(buyerId)
            .ValidateItems();

    public static ActiveCart CreateWithId(ActiveCartId id, AccountId buyerId)
        => new ActiveCart(buyerId)
        {
            Id = id
        }
        .ValidateItems();

    public ActiveCartItem AddItem(double weight, ProductId productId, bool forDelivery)
    {
        var item = ActiveCartItem.Create(weight, productId, Id, forDelivery);
        items.Add(item);
        this.ValidateItems();

        return item;
    }

    public ActiveCartItem RemoveItem(ActiveCartItem item)
    {
        items.Remove(item);
        this.ValidateItems();

        return item;
    }

    public int RemoveItemsByProductId(ProductId id)
    {
        var removedCount = items.RemoveAll(i => i.ProductId == id);
        this.ValidateItems();

        return removedCount;
    }
}
