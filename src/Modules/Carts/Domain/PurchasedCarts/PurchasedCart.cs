using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Common.Exceptions.PurchasedCarts.Carts;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Carts.Domain.PurchasedCarts.Validation;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
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
        PurchaseDate = DateTime.UtcNow;
    }

    public PurchasedCartId Id { get; init; }
    public DateTime PurchaseDate { get; }
    public AccountId BuyerId { get; private set; }
    public ShipmentId? ShipmentId { get; private set; }
    public decimal TotalCost => items.Sum(i => i.Price);
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

    public PurchasedCartItem[] AddItems((decimal Price, CadId CadId, ActiveCartItem Item)[] items)
    {
        this.items.AddRange([.. items.Select(i => PurchasedCartItem.Create(
            cartId: this.Id,
            productId: i.Item.ProductId,
            cadId: i.CadId,
            price: i.Price,
            quantity: i.Item.Quantity,
            forDelivery: i.Item.ForDelivery
        ))]);
        this.ValidateItems();

        return [.. this.items];
    }

    public PurchasedCart SetShipmentId(ShipmentId shipmentId)
    {
        if (!HasDelivery)
        {
            throw PurchasedCartValidationException.ShipmentIdOnCartWithNoDelivery();
        }
        ShipmentId = shipmentId;

        return this;
    }
}
