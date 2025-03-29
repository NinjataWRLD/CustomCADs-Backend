using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Carts.Domain.PurchasedCarts;

public class PurchasedCart : BaseAggregateRoot
{
    private readonly List<PurchasedCartItem> items = [];

    private PurchasedCart() { }
    private PurchasedCart(AccountId buyerId) : this()
    {
        BuyerId = buyerId;
        PurchasedAt = DateTimeOffset.UtcNow;
    }

    public PurchasedCartId Id { get; init; }
    public DateTimeOffset PurchasedAt { get; }
    public AccountId BuyerId { get; private set; }
    public ShipmentId? ShipmentId { get; private set; }
    public decimal TotalCost => items.Sum(i => i.Cost);
    public bool HasDelivery => items.Any(i => i.ForDelivery);
    public IReadOnlyCollection<PurchasedCartItem> Items => items.AsReadOnly();

    public static PurchasedCart Create(AccountId buyerId)
        => new(buyerId);

    public static PurchasedCart CreateWithId(PurchasedCartId id, AccountId buyerId)
        => new PurchasedCart(buyerId)
        {
            Id = id
        }
        .ValidateItems();

    public PurchasedCartItem[] AddItems(
        (
            decimal Price, 
            CadId CadId, 
            ProductId ProductId, 
            bool ForDelivery,   
            CustomizationId? CustomizationId, 
            int Quantity
        )[] items
    )
    {
        this.items.AddRange([.. items.Select(i => PurchasedCartItem.Create(
            cartId: this.Id,
            productId: i.ProductId,
            cadId: i.CadId,
            customizationId: i.CustomizationId,
            price: i.Price,
            quantity: i.Quantity,
            forDelivery: i.ForDelivery
        ))]);
        this.ValidateItems();

        return [.. this.items];
    }

    public PurchasedCart SetShipmentId(ShipmentId shipmentId)
    {
        if (!HasDelivery)
        {
            throw CustomValidationException<PurchasedCart>.Custom("Cannot set ShipmentId on a Purchased Cart with no requested Delivery");
        }
        ShipmentId = shipmentId;

        return this;
    }
}
