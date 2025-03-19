using CustomCADs.Carts.Domain.ActiveCarts.Exceptions.CartItems;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Carts.Domain.ActiveCarts.Entities;

public class ActiveCartItem : BaseEntity
{
    private ActiveCartItem() { }
    private ActiveCartItem(
        bool forDelivery,
        ActiveCartId cartId,
        ProductId productId,
        CustomizationId? customizationId) : this()
    {
        ForDelivery = forDelivery;
        CartId = cartId;
        ProductId = productId;
        CustomizationId = customizationId;
    }

    public int Quantity { get; private set; } = 1;
    public bool ForDelivery { get; set; }
    public ProductId ProductId { get; }
    public CustomizationId? CustomizationId { get; private set; }
    public ActiveCartId CartId { get; }
    public ActiveCart Cart { get; } = null!;

    public static ActiveCartItem Create(ProductId productId, ActiveCartId cartId)
        => new(
            forDelivery: false,
            cartId: cartId,
            productId: productId,
            customizationId: null
        );

    public static ActiveCartItem Create(ProductId productId, ActiveCartId cartId, CustomizationId customizationId)
        => new(
            forDelivery: true,
            cartId: cartId,
            productId: productId,
            customizationId: customizationId
        );

    public ActiveCartItem IncreaseQuantity(int amount)
    {
        if (!ForDelivery)
            throw ActiveCartItemValidationException.EditQuantityOnNonDelivery(ProductId);

        Quantity += amount;
        this.ValidateQuantity();

        return this;
    }

    public ActiveCartItem DecreaseQuantity(int amount)
    {
        if (!ForDelivery)
            throw ActiveCartItemValidationException.EditQuantityOnNonDelivery(ProductId);

        Quantity -= amount;
        this.ValidateQuantity();

        return this;
    }

    public ActiveCartItem SetForDelivery(CustomizationId customizationId)
    {
        ForDelivery = true;
        CustomizationId = customizationId;

        return this;
    }

    public ActiveCartItem SetNoDelivery()
    {
        ForDelivery = false;
        CustomizationId = null;
        Quantity = 1;

        return this;
    }
}
