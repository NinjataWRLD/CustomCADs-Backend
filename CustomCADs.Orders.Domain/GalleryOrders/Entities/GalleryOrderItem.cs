using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;

namespace CustomCADs.Orders.Domain.GalleryOrders.Entities;

public class GalleryOrderItem : BaseEntity
{
    private GalleryOrderItem() { }
    private GalleryOrderItem(Money price, int quantity, ProductId productId, GalleryOrderId cartId) : this()
    {
        Price = price;
        Quantity = quantity;
        PurchaseDate = DateTime.UtcNow;
        ProductId = productId;
        CartId = cartId;
    }

    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Money Price { get; set; } = new();
    public DateTime PurchaseDate { get; set; }
    public ProductId ProductId { get; set; }
    public GalleryOrderId CartId { get; set; }
    public GalleryOrder Cart { get; set; } = null!;

    public Money Cost => Price.Multiply(Quantity);

    public static GalleryOrderItem Create(Money price, int quantity, ProductId productId, GalleryOrderId cartId)
        => new GalleryOrderItem(price, quantity, productId, cartId)
            .ValidateQuantity()
            .ValidatePriceAmount();
}
