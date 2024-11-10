using CustomCADs.Cart.Domain.Cart.Entities;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Cart.Domain.ItemCads;

public class ItemCad : IAggregateRoot
{
    private ItemCad() { }
    private ItemCad(Cad cad, Guid itemId) : this()
    {
        Cad = cad;
        ItemId = itemId;
    }

    public Guid Id { get; set; }
    public Cad Cad { get; set; } = new();
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public static ItemCad Create(Guid itemId, Cad cad)
    {
        return new(cad, itemId);
    }
}
