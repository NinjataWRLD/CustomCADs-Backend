using CustomCADs.Carts.Domain.Carts.Entities;
using CustomCADs.Carts.Domain.Carts.Validation;
using CustomCADs.Carts.Domain.Common.Exceptions.CartItems;
using CustomCADs.Carts.Domain.Common.Exceptions.Carts;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Carts.Domain.Carts;

public class Cart : BaseAggregateRoot
{
    private readonly List<CartItem> items = [];

    private Cart() { }
    private Cart(AccountId buyerId) : this()
    {
        BuyerId = buyerId;
        PurchaseDate = DateTime.UtcNow;
        Total = Items.Sum(i => i.Price);
    }

    public CartId Id { get; init; }
    public decimal Total { get; private set; }
    public DateTime PurchaseDate { get; }
    public AccountId BuyerId { get; private set; }
    public ShipmentId? ShipmentId { get; private set; }
    public IReadOnlyCollection<CartItem> Items => items.AsReadOnly();
    public bool Delivery => items.Any(i => i.Delivery);

    public static Cart Create(AccountId buyerId)
        => new Cart(buyerId)
            .ValidateItems();

    public CartItem AddItem(decimal price, int quantity, ProductId productId, bool delivery)
    {
        var item = CartItem.Create(price, quantity, productId, Id, delivery);
        items.Add(item);
        this.ValidateItems();

        Total += item.Cost;
        return item;
    }

    public CartItem RemoveItem(CartItemId id)
    {
        var item = items.FirstOrDefault(i => i.Id == id) ?? throw CartItemNotFoundException.ById(id);
        items.Remove(item);
        this.ValidateItems();

        Total -= item.Cost;
        return item;
    }

    public Cart SetShipmentId(ShipmentId shipmentId)
    {
        if (!Delivery)
        {
            throw CartValidationException.ShipmentIdOnCartWithNoDelivery();
        }
        ShipmentId = shipmentId;

        return this;
    }
}
