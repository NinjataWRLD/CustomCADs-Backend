﻿using CustomCADs.Cart.Domain.Cart.Entities;
using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Cart.Domain.Cart;

public class Cart : BaseAggregateRoot
{
    private Cart() { }
    private Cart(Guid buyerId, ICollection<Item> items) : this()
    {
        BuyerId = buyerId;
        Items = items;
        Total = Items.Sum(i => i.Price.Amount);
    }

    public Guid Id { get; set; }
    public decimal Total { get; private set; }
    public Guid BuyerId { get; set; }
    public ICollection<Item> Items { get; set; } = [];

    public static Cart Create(Guid buyerId, ICollection<Item> items)
    {
        return new(buyerId, items);
    }
}
