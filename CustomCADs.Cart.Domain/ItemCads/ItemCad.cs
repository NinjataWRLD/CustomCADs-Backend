using CustomCADs.Cart.Domain.Cart.Entities;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Cart.Domain.ItemCads;

public class ItemCad : IAggregateRoot
{
    public Guid Id { get; set; }
    public Cad Cad { get; set; } = new();
    public Guid ItemId { get; set; }
    public required Item Item { get; set; }
}
