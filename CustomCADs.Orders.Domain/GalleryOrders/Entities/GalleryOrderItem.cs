using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;

namespace CustomCADs.Orders.Domain.GalleryOrders.Entities;

public class GalleryOrderItem : BaseEntity
{
    private GalleryOrderItem() { }
    private GalleryOrderItem(Money price, int quantity, ProductId productId, GalleryOrderId galleryOrderId) : this()
    {
        Price = price;
        Quantity = quantity;
        PurchaseDate = DateTime.UtcNow;
        ProductId = productId;
        GalleryOrderId = galleryOrderId;
    }

    public GalleryOrderItemId Id { get; init; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; } = new();
    public DateTime PurchaseDate { get; }
    public ProductId ProductId { get; }
    public GalleryOrderId GalleryOrderId { get; }
    public GalleryOrder GalleryOrder { get; } = null!;

    public Money Cost => Price.Multiply(Quantity);

    public static GalleryOrderItem Create(Money price, int quantity, ProductId productId, GalleryOrderId galleryOrderId)
        => new GalleryOrderItem(price, quantity, productId, galleryOrderId)
            .ValidateQuantity()
            .ValidatePriceAmount();

    public GalleryOrderItem SetQuantity(int quantity)
    {
        Quantity = quantity;
        this.ValidateQuantity();
        return this;
    }

    public GalleryOrderItem SetPrice(Money price)
    {
        Price = price;
        this.ValidatePriceAmount();
        return this;
    }
}
