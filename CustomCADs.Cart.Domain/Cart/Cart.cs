using CustomCADs.Cart.Domain.Cart.Entities;
using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Cart.Domain.Cart;

public class Cart : IAggregateRoot
{
    private Cart() { }
    private Cart(Guid buyerId, ICollection<Item> items) : this()
    {
        BuyerId = buyerId;
        Items = items;
    }

    public Guid Id { get; set; }
    public Guid BuyerId { get; set; }
    public ICollection<Item> Items { get; set; } = [];

    public decimal Total => Items.Sum(i => i.Cost);

    public static Cart Create(Guid buyerId, ICollection<Item> items)
    {
        return new(buyerId, items);
    }
}
