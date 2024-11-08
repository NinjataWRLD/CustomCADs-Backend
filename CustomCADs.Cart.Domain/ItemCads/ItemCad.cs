using CustomCADs.Cart.Domain.ItemCads.ValueObjects;

namespace CustomCADs.Cart.Domain.ItemCads;

public class ItemCad
{
    public Guid Id { get; set; }
    public Cad Cad { get; set; } = new();
    public Guid ItemId { get; set; }
}
