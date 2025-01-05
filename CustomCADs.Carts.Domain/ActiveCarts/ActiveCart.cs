using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Validation;
using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.CartItems;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Carts.Domain.ActiveCarts;

public class ActiveCart : BaseAggregateRoot
{
    private readonly List<ActiveCartItem> items = [];

    private ActiveCart() { }
    private ActiveCart(AccountId buyerId) : this()
    {
        BuyerId = buyerId;
    }

    public ActiveCartId Id { get; init; }
    public AccountId BuyerId { get; private set; }
    public bool HasDelivery => items.Any(i => i.Delivery);
    public double TotalDeliveryWeight => Items.Where(i => i.Delivery).Sum(i => i.Weight);
    public int TotalDeliveryCount => Items.Where(i => i.Delivery).Count();
    public int TotalCount => Items.Count;
    public IReadOnlyCollection<ActiveCartItem> Items => items.AsReadOnly();

    public static ActiveCart Create(AccountId buyerId)
        => new ActiveCart(buyerId)
            .ValidateItems();

    public ActiveCartItem AddItem(double weight, ProductId productId, bool delivery)
    {
        var item = ActiveCartItem.Create(weight, productId, Id, delivery);
        items.Add(item);
        this.ValidateItems();

        return item;
    }

    public ActiveCartItem RemoveItem(ActiveCartItemId id)
    {
        ActiveCartItem item = items.FirstOrDefault(i => i.Id == id)
            ?? throw ActiveCartItemNotFoundException.ById(id);

        items.Remove(item);
        this.ValidateItems();

        return item;
    }

    public int RemoveItemsByProductId(ProductId id)
    {
        var removedCount = items.RemoveAll(i => i.ProductId == id);
        this.ValidateItems();

        return removedCount;
    }
}
