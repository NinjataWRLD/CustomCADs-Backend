using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Cart.Domain.Cart.Entities;

public class Item : BaseEntity
{
    private Item() { }
    private Item(decimal price, int quantity, Purpose purpose, Guid productId, Guid cartId) : this()
    {
        Price = price;
        Quantity = quantity;
        PurchaseDate = DateTime.UtcNow;
        Purpose = purpose;
        ProductId = productId;
        CartId = cartId;
    }

    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime PurchaseDate { get; set; }
    public Purpose Purpose { get; set; }
    public Guid ProductId { get; set; }
    public Guid CartId { get; set; }
    public Cart Cart { get; set; } = null!;

    public decimal Cost => Price * Quantity;

    public static Item Create(decimal price, int quantity, Purpose purpose, Guid productId, Guid cartId)
    {
        return new(price, quantity, purpose, productId, cartId);
    }
}
