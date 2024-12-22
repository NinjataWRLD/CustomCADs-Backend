using CustomCADs.Carts.Domain.Carts.Validation;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.Carts.Entities;

public class CartItem : BaseEntity
{
    private CartItem() { }
    private CartItem(
        decimal price,
        int quantity,
        double weight,
        ProductId productId,
        CartId cartId,
        bool delivery) : this()
    {
        Price = price;
        Weight = weight;
        Quantity = quantity;
        ProductId = productId;
        CartId = cartId;
        Delivery = delivery;
    }

    public CartItemId Id { get; init; }
    public int Quantity { get; private set; }
    public double Weight { get; private set; }
    public decimal Price { get; private set; }
    public bool Delivery { get; set; }
    public DateTime PurchaseDate { get; }
    public ProductId ProductId { get; }
    public CartId CartId { get; }
    public Cart Cart { get; } = null!;
    public CadId? CadId { get; private set; }
    public decimal Cost => Price * Quantity;

    public static CartItem Create(decimal price, int quantity, double weight, ProductId productId, CartId cartId, bool delivery)
        => new CartItem(price, quantity, weight, productId, cartId, delivery)
            .ValidateQuantity()
            .ValidateWeight()
            .ValidatePrice();

    public CartItem SetQuantity(int quantity)
    {
        Quantity = quantity;
        this.ValidateQuantity();
        return this;
    }

    public CartItem SetPrice(decimal price)
    {
        Price = price;
        this.ValidatePrice();
        return this;
    }

    public CartItem SetCadId(CadId cadId)
    {
        CadId = cadId;

        return this;
    }
}
