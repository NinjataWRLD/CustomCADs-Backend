using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Carts.Domain.PurchasedCarts.Entities;

public class PurchasedCartItem : BaseEntity
{
    private PurchasedCartItem() { }
    private PurchasedCartItem(
        PurchasedCartId cartId,
        ProductId productId,
        CadId cadId,
        CustomizationId? customizationId,
        decimal price,
        int quantity,
        bool forDelivery) : this()
    {
        CartId = cartId;
        ProductId = productId;
        CadId = cadId;
        CustomizationId = customizationId;
        Price = price;
        Quantity = quantity;
        ForDelivery = forDelivery;
    }

    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public bool ForDelivery { get; set; }
    public ProductId ProductId { get; }
    public CadId CadId { get; private set; }
    public CustomizationId? CustomizationId { get; }
    public PurchasedCartId CartId { get; }
    public PurchasedCart Cart { get; } = null!;
    public decimal Cost => Price * Quantity;

    public static PurchasedCartItem Create(
        PurchasedCartId cartId,
        ProductId productId,
        CadId cadId,
        CustomizationId? customizationId,
        decimal price,
        int quantity,
        bool forDelivery
    ) => new PurchasedCartItem(
            cartId,
            productId,
            cadId,
            customizationId,
            price,
            quantity,
            forDelivery
        )
        .ValidateQuantity()
        .ValidatePrice();
}
