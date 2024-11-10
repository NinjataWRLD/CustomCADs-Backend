using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Cart.Domain.Cart.Entities;

public class Item : BaseEntity
{
    private Item() { }
    private Item(Money price, int quantity, Purpose purpose, Guid productId, Guid cartId) : this()
    {
        Price = price;
        Quantity = quantity;
        PurchaseDate = DateTime.UtcNow;
        Purpose = purpose;
        ProductId = productId;
        CartId = cartId;
    }

    public Guid Id { get; set; }
    public Money Price { get; set; } = new();
    public int Quantity { get; set; }
    public DateTime PurchaseDate { get; set; }
    public Purpose Purpose { get; set; }
    public Guid ProductId { get; set; }
    public Guid CartId { get; set; }
    public Cart Cart { get; set; } = null!;

    public Money Cost => Price.Multiply(Quantity);

    public static Item Create(Money price, int quantity, Purpose purpose, Guid productId, Guid cartId)
    {
        return new(price, quantity, purpose, productId, cartId);
    }
}
