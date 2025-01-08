using CustomCADs.Carts.Domain.ActiveCarts.Validation;
using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.CartItems;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.ActiveCarts.Entities;

public class ActiveCartItem : BaseEntity
{
    private ActiveCartItem() { }
    private ActiveCartItem(
        double weight,
        ProductId productId,
        ActiveCartId cartId,
        bool forDelivery) : this()
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = 1;
        Weight = weight;
        ForDelivery = forDelivery;
    }

    public ActiveCartItemId Id { get; init; }
    public int Quantity { get; private set; }
    public double Weight { get; private set; }
    public bool ForDelivery { get; set; }
    public ProductId ProductId { get; }
    public ActiveCartId CartId { get; }
    public ActiveCart Cart { get; } = null!;

    public static ActiveCartItem Create(double weight, ProductId productId, ActiveCartId cartId, bool forDelivery)
        => new ActiveCartItem(weight, productId, cartId, forDelivery)
            .ValidateQuantity()
            .ValidateWeight();

    public static ActiveCartItem CreateWithId(ActiveCartItemId id, double weight, ProductId productId, ActiveCartId cartId, bool forDelivery)
        => new ActiveCartItem(weight, productId, cartId, forDelivery)
        {
            Id = id
        }
        .ValidateQuantity()
        .ValidateWeight();

    public ActiveCartItem IncreaseQuantity(int amount)
    {
        if (!ForDelivery)
            throw ActiveCartItemValidationException.EditQuantityOnNonDelivery(Id);

        Quantity += amount;
        this.ValidateQuantity();

        return this;
    }

    public ActiveCartItem DecreaseQuantity(int amount)
    {
        if (!ForDelivery)
            throw ActiveCartItemValidationException.EditQuantityOnNonDelivery(Id);

        Quantity -= amount;
        this.ValidateQuantity();

        return this;
    }

    public ActiveCartItem SetForDelivery(bool forDelivery)
    {
        ForDelivery = forDelivery;

        return this;
    }
}
