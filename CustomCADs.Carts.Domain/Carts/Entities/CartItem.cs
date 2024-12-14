using CustomCADs.Carts.Domain.Carts.Validation;
using CustomCADs.Carts.Domain.Common.Exceptions.CartItems;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Carts.Domain.Carts.Entities;

public class CartItem : BaseEntity
{
    private CartItem() { }
    private CartItem(
        decimal price,
        int quantity,
        ProductId productId,
        CartId cartId,
        ShipmentId? shipmentId) : this()
    {
        Price = price;
        Quantity = quantity;
        PurchaseDate = DateTime.UtcNow;
        ProductId = productId;
        CartId = cartId;
        ShipmentId = shipmentId;
    }

    public CartItemId Id { get; init; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public DateTime PurchaseDate { get; }
    public ProductId ProductId { get; }
    public CartId CartId { get; }
    public Cart Cart { get; } = null!;
    public CadId? CadId { get; private set; }
    public ShipmentId? ShipmentId { get; private set; }
    public decimal Cost => Price * Quantity;
    public bool Delivery => ShipmentId is not null;

    public static CartItem Create(decimal price, int quantity, ProductId productId, CartId cartId, ShipmentId? shipmentId)
        => shipmentId is not null
            ? CreatePhysical(price, quantity, productId, cartId, shipmentId.Value)
            : CreateDigital(price, quantity, productId, cartId);

    private static CartItem CreateDigital(decimal price, int quantity, ProductId productId, CartId cartId)
        => new CartItem(price, quantity, productId, cartId, shipmentId: null)
            .ValidateQuantity()
            .ValidatePrice();

    private static CartItem CreatePhysical(decimal price, int quantity, ProductId productId, CartId cartId, ShipmentId shipmentId)
        => new CartItem(price, quantity, productId, cartId, shipmentId)
            .ValidateQuantity()
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
        if (Delivery)
        {
            throw CartItemValidationException.CadIdOnNonDigitalDeliveryType();
        }
        CadId = cadId;

        return this;
    }

    public CartItem SetShipmentId(ShipmentId shipmentId)
    {
        if (!Delivery)
        {
            throw CartItemValidationException.ShipmentIdOnNonPhysicalDeliveryType();
        }
        ShipmentId = shipmentId;

        return this;
    }
}
