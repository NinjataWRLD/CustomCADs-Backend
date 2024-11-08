using CustomCADs.Shared.Core.Entities;

namespace CustomCADs.Cart.Domain.Items;

public class Item : IEntity
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime PurchaseDate { get; set; }
    public Guid ProductId { get; set; }
    public Guid? CartId { get; set; }

    public decimal Cost => Price * Quantity;
}
