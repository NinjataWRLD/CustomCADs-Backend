﻿using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Gallery.Domain.Carts.Validation;
using CustomCADs.Gallery.Domain.Common.Exceptions.CartItems;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Domain.Carts.Entities;

public class Cart : BaseAggregateRoot
{
    private readonly List<CartItem> items = [];

    private Cart() { }
    private Cart(UserId buyerId) : this()
    {
        BuyerId = buyerId;
        PurchaseDate = DateTime.UtcNow;
        Total = Items.Sum(i => i.Price.Amount);
    }

    public CartId Id { get; init; }
    public decimal Total { get; private set; }
    public DateTime PurchaseDate { get; }
    public UserId BuyerId { get; private set; }
    public IReadOnlyCollection<CartItem> Items => items.AsReadOnly();

    public static Cart Create(UserId buyerId)
        => new Cart(buyerId)
            .ValidateItems();

    public Cart AddItem(DeliveryType type, Money price, int quantity, ProductId productId)
    {
        var item = CartItem.Create(type, price, quantity, productId, Id);
        items.Add(item);

        Total += item.Cost.Amount;
        return this;
    }

    public Cart RemoveItem(CartItemId id)
    {
        var item = items.FirstOrDefault(i => i.Id == id) ?? throw CartItemNotFoundException.ById(id);
        items.Remove(item);

        Total += item.Cost.Amount;
        return this;
    }
}