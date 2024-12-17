using CustomCADs.Carts.Domain.Carts.Entities;
using CustomCADs.Carts.Domain.Carts.Validation;
using CustomCADs.Carts.Domain.Common.Exceptions.CartItems;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

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
    public IReadOnlyCollection<CartItem> Items => items.AsReadOnly();

    public static Cart Create(AccountId buyerId)
        => new Cart(buyerId)
            .ValidateItems();

    public CartItem AddItem(decimal price, int quantity, ProductId productId, bool delivery)
    {
        var item = CartItem.Create(price, quantity, productId, Id, delivery);
        items.Add(item);

        Total += item.Cost;
        return item;
    }

    public CartItem RemoveItem(CartItemId id)
    {
        var item = items.FirstOrDefault(i => i.Id == id) ?? throw CartItemNotFoundException.ById(id);
        items.Remove(item);

        Total -= item.Cost;
        return item;
    }
}
