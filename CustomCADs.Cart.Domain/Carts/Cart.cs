using CustomCADs.Cart.Domain.Carts.Entities;
using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Cart.Domain.Carts;

public class Cart : BaseAggregateRoot
{
    private Cart() { }
    private Cart(Guid buyerId, ICollection<Item> items) : this()
    {
        BuyerId = buyerId;
        Items = items;
        PurchaseDate = DateTime.UtcNow;
        Total = Items.Sum(i => i.Price.Amount);
    }

    public CartId Id { get; set; }
    public decimal Total { get; private set; }
    public DateTime PurchaseDate { get; set; }
    public Guid BuyerId { get; set; }
    public ICollection<Item> Items { get; set; } = [];

    public static Cart Create(Guid buyerId, ICollection<Item> items)
    {
        return new(buyerId, items);
    }
}
