using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;

namespace CustomCADs.Cart.Domain.Carts.Entities;

public class Item : BaseEntity
{
    private Item() { }
    private Item(Money price, int quantity, Delivery deilvery, ProductId productId, CartId cartId) : this()
    {
        Price = price;
        Quantity = quantity;
        PurchaseDate = DateTime.UtcNow;
        Delivery = deilvery;
        ProductId = productId;
        CartId = cartId;
    }

    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Money Price { get; set; } = new();
    public DateTime PurchaseDate { get; set; }
    public Delivery Delivery { get; set; } = Delivery.CreateNone();
    public ProductId ProductId { get; set; }
    public CartId CartId { get; set; }
    public Cart Cart { get; set; } = null!;

    public Money Cost => Price.Multiply(Quantity);

    public static Item Create(Money price, int quantity, Delivery delivery, ProductId productId, CartId cartId)
        => new Item(price, quantity, delivery, productId, cartId)
            .ValidateQuantity()
            .ValidatePriceAmount();
}
