using CustomCADs.Orders.Domain.Carts.Validation;
using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.Common.Exceptions.Carts.GalleryOrders;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;

namespace CustomCADs.Orders.Domain.Carts.Entities;

public class Cart : BaseAggregateRoot
{
    private readonly List<GalleryOrder> orders = [];

    private Cart() { }
    private Cart(UserId buyerId) : this()
    {
        BuyerId = buyerId;
        PurchaseDate = DateTime.UtcNow;
        Total = Orders.Sum(i => i.Price.Amount);
    }

    public CartId Id { get; init; }
    public decimal Total { get; private set; }
    public DateTime PurchaseDate { get; }
    public UserId BuyerId { get; private set; }
    public IReadOnlyCollection<GalleryOrder> Orders => orders.AsReadOnly();

    public static Cart CreateDigital(UserId buyerId)
        => new Cart(buyerId)
            .ValidateOrders();

    public static Cart CreatePhysical(UserId buyerId)
        => new Cart(buyerId)
            .ValidateOrders();

    public static Cart CreateDigitalAndPhysical(UserId buyerId)
        => new Cart(buyerId)
            .ValidateOrders();

    public Cart AddOrder(DeliveryType type, Money price, int quantity, ProductId productId)
    {
        orders.Add(GalleryOrder.Create(type, price, quantity, productId, Id));
        return this;
    }

    public Cart RemoveOrder(GalleryOrderId id)
    {
        orders.Remove(orders.FirstOrDefault(i => i.Id == id)
            ?? throw GalleryOrderNotFoundException.ById(id));
        return this;
    }
}
