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

    public GalleryOrderItemId Id { get; set; }
    public int Quantity { get; set; }
    public Money Price { get; set; } = new();
    public DateTime PurchaseDate { get; set; }
    public ProductId ProductId { get; set; }
    public GalleryOrderId GalleryOrderId { get; set; }
    public GalleryOrder GalleryOrder { get; set; } = null!;

    public Money Cost => Price.Multiply(Quantity);

    public static GalleryOrderItem Create(Money price, int quantity, ProductId productId, GalleryOrderId galleryOrderId)
        => new GalleryOrderItem(price, quantity, productId, galleryOrderId)
            .ValidateQuantity()
            .ValidatePriceAmount();
}
