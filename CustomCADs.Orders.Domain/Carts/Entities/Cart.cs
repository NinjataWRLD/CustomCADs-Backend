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

    public static Cart Create(UserId buyerId)
        => new Cart(buyerId)
            .ValidateOrders();

    public Cart AddOrder(DeliveryType type, Money price, int quantity, ProductId productId)
    {
        var order = GalleryOrder.Create(type, price, quantity, productId, Id);
        orders.Add(order);
        
        Total += order.Cost.Amount;
        return this;
    }

    public Cart RemoveOrder(GalleryOrderId id)
    {
        var order = orders.FirstOrDefault(i => i.Id == id) ?? throw GalleryOrderNotFoundException.ById(id);
        orders.Remove(order);
        
        Total += order.Cost.Amount;
        return this;
    }
}
