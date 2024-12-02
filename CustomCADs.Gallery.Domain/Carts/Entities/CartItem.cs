using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Gallery.Domain.Carts.Validation;
using CustomCADs.Gallery.Domain.Common.Exceptions.CartItems;
using CustomCADs.Gallery.Domain.Common.Exceptions.Carts;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Gallery.Domain.Carts.Entities;

public class CartItem : BaseEntity
{
    private CartItem() { }
    private CartItem(DeliveryType deliveryType, decimal price, int quantity, ProductId productId, CartId cartId) : this()
    {
        Price = price;
        Quantity = quantity;
        DeliveryType = deliveryType;
        PurchaseDate = DateTime.UtcNow;
        ProductId = productId;
        CartId = cartId;
    }

    public CartItemId Id { get; init; }
    public int Quantity { get; private set; }
    public DeliveryType DeliveryType { get; }
    public decimal Price { get; private set; }
    public DateTime PurchaseDate { get; }
    public ProductId ProductId { get; }
    public CartId CartId { get; }
    public Cart Cart { get; } = null!;
    public CadId? CadId { get; private set; }
    public ShipmentId? ShipmentId { get; private set; }
    public decimal Cost => Price * Quantity;

    public static CartItem Create(DeliveryType type, decimal price, int quantity, ProductId productId, CartId cartId)
    {
        return type switch
        {
            DeliveryType.Digital => CreateDigital(price, quantity, productId, cartId),
            DeliveryType.Physical => CreatePhysical(price, quantity, productId, cartId),
            DeliveryType.Both => CreateDigitalAndPhysical(price, quantity, productId, cartId),
            _ => throw CartValidationException.General(),
        };
    }

    public static CartItem CreateDigital(decimal price, int quantity, ProductId productId, CartId cartId)
        => new CartItem(DeliveryType.Digital, price, quantity, productId, cartId)
            .ValidateQuantity()
            .ValidatePrice();

    public static CartItem CreatePhysical(decimal price, int quantity, ProductId productId, CartId cartId)
        => new CartItem(DeliveryType.Physical, price, quantity, productId, cartId)
            .ValidateQuantity()
            .ValidatePrice();

    public static CartItem CreateDigitalAndPhysical(decimal price, int quantity, ProductId productId, CartId cartId)
        => new CartItem(DeliveryType.Both, price, quantity, productId, cartId)
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
        if (DeliveryType is DeliveryType.Digital or DeliveryType.Both)
        {
            CadId = cadId;
        }
        else throw CartItemValidationException.CadIdOnNonDigitalDeliveryType();

        return this;
    }

    public CartItem SetShipmentId(ShipmentId shipmentId)
    {
        if (DeliveryType is DeliveryType.Physical or DeliveryType.Both)
        {
            ShipmentId = shipmentId;
        }
        else throw CartItemValidationException.ShipmentIdOnNonPhysicalDeliveryType();

        return this;
    }
}
