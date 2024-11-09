using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Cart.Domain.ItemCads;

public class ItemCad : IEntity
{
    public Guid Id { get; set; }
    public Cad Cad { get; set; } = new();
    public Guid ItemId { get; set; }
}
