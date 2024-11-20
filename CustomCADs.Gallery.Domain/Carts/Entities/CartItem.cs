using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Gallery.Domain.Carts.Validation;
using CustomCADs.Gallery.Domain.Common.Exceptions.CartItems;
using CustomCADs.Gallery.Domain.Common.Exceptions.Carts;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Shipments;

namespace CustomCADs.Gallery.Domain.Carts.Entities;

public class CartItem : BaseEntity
{
    private CartItem() { }
    private CartItem(DeliveryType deliveryType, Money price, int quantity, ProductId productId, CartId cartId) : this()
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
    public Money Price { get; private set; } = new();
    public DateTime PurchaseDate { get; }
    public ProductId ProductId { get; }
    public CartId CartId { get; }
    public Cart Cart { get; } = null!;
    public CadId? CadId { get; private set; }
    public ShipmentId? ShipmentId { get; private set; }
    public Money Cost => Price.Multiply(Quantity);

    public static CartItem Create(DeliveryType type, Money price, int quantity, ProductId productId, CartId cartId)
    {
        return type switch
        {
            DeliveryType.Digital => CreateDigital(price, quantity, productId, cartId),
            DeliveryType.Physical => CreatePhysical(price, quantity, productId, cartId),
            DeliveryType.Both => CreateDigitalAndPhysical(price, quantity, productId, cartId),
            _ => throw CartValidationException.General(),
        };
    }

    public static CartItem CreateDigital(Money price, int quantity, ProductId productId, CartId cartId)
        => new CartItem(DeliveryType.Digital, price, quantity, productId, cartId)
            .ValidateQuantity()
            .ValidatePriceAmount();

    public static CartItem CreatePhysical(Money price, int quantity, ProductId productId, CartId cartId)
        => new CartItem(DeliveryType.Physical, price, quantity, productId, cartId)
            .ValidateQuantity()
            .ValidatePriceAmount();

    public static CartItem CreateDigitalAndPhysical(Money price, int quantity, ProductId productId, CartId cartId)
        => new CartItem(DeliveryType.Both, price, quantity, productId, cartId)
            .ValidateQuantity()
            .ValidatePriceAmount();

    public CartItem SetQuantity(int quantity)
    {
        Quantity = quantity;
        this.ValidateQuantity();
        return this;
    }

    public CartItem SetPrice(Money price)
    {
        Price = price;
        this.ValidatePriceAmount();
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
