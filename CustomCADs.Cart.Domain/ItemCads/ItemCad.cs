using CustomCADs.Cart.Domain.ItemCads.ValueObjects;
using CustomCADs.Shared.Core.Entities;

namespace CustomCADs.Cart.Domain.ItemCads;

public class ItemCad : IEntity
{
    public Guid Id { get; set; }
    public Cad Cad { get; set; } = new();
    public Guid ItemId { get; set; }
}
