using CustomCADs.Cart.Domain.Cart.Entities;
using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Cart.Domain.Cart;

public class Cart : IEntity
{
    public Guid Id { get; set; }
    public Guid BuyerId { get; set; }
    public ICollection<Item> Items { get; set; } = [];

    public decimal Total => Items.Sum(i => i.Cost);
}
