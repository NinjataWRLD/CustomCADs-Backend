using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.GalleryOrders.Entities;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.GalleryOrders;

public class GalleryOrder : BaseAggregateRoot
{
    private GalleryOrder() { }
    private GalleryOrder(DeliveryType deliveryType, UserId buyerId, ICollection<GalleryOrderItem> items) : this()
    {
        DeliveryType = deliveryType;
        BuyerId = buyerId;
        Items = items;
        PurchaseDate = DateTime.UtcNow;
        Total = Items.Sum(i => i.Price.Amount);
    }

    public GalleryOrderId Id { get; set; }
    public decimal Total { get; private set; }
    public DeliveryType DeliveryType { get; set; }
    public DateTime PurchaseDate { get; set; }
    public UserId BuyerId { get; set; }
    public ICollection<GalleryOrderItem> Items { get; set; } = [];

    public static GalleryOrder Create(DeliveryType deliveryType, UserId buyerId, ICollection<GalleryOrderItem> items)
        => new GalleryOrder(deliveryType, buyerId, items)
            .ValidateItems();
}
