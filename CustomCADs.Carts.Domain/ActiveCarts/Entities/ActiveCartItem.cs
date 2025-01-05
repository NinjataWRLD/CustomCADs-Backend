using CustomCADs.Carts.Domain.ActiveCarts.Validation;
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
        bool delivery) : this()
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = 1;
        Weight = weight;
        Delivery = delivery;
    }

    public ActiveCartItemId Id { get; init; }
    public int Quantity { get; private set; }
    public double Weight { get; private set; }
    public bool Delivery { get; set; }
    public ProductId ProductId { get; }
    public ActiveCartId CartId { get; }
    public ActiveCart Cart { get; } = null!;

    public static ActiveCartItem Create(double weight, ProductId productId, ActiveCartId cartId, bool delivery)
        => new ActiveCartItem(weight, productId, cartId, delivery)
            .ValidateQuantity()
            .ValidateWeight();

    public ActiveCartItem IncrementQuantity()
    {
        Quantity++;
        this.ValidateQuantity();
        return this;
    }

    public ActiveCartItem DecrementQuantity()
    {
        Quantity--;
        this.ValidateQuantity();
        return this;
    }

    public ActiveCartItem TurnDeliveryOn()
    {
        Delivery = true;

        return this;
    }

    public ActiveCartItem TurnDeliveryOff()
    {
        Delivery = false;

        return this;
    }
}
