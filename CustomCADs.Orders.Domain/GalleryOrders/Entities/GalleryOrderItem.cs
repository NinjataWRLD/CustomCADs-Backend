using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders;
using CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders.Items;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;

namespace CustomCADs.Orders.Domain.GalleryOrders.Entities;

public class GalleryOrderItem : BaseEntity
{
    private GalleryOrderItem() { }
    private GalleryOrderItem(DeliveryType deliveryType, Money price, int quantity, ProductId productId, GalleryOrderId galleryOrderId) : this()
    {
        Price = price;
        Quantity = quantity;
        DeliveryType = deliveryType;
        PurchaseDate = DateTime.UtcNow;
        ProductId = productId;
        GalleryOrderId = galleryOrderId;
    }

    public GalleryOrderItemId Id { get; init; }
    public int Quantity { get; private set; }
    public DeliveryType DeliveryType { get; }
    public Money Price { get; private set; } = new();
    public DateTime PurchaseDate { get; }
    public ProductId ProductId { get; }
    public GalleryOrderId GalleryOrderId { get; }
    public GalleryOrder GalleryOrder { get; } = null!;
    public CadId? CadId { get; private set; }
    public ShipmentId? ShipmentId { get; private set; }


    public Money Cost => Price.Multiply(Quantity);

    public static GalleryOrderItem Create(DeliveryType type, Money price, int quantity, ProductId productId, GalleryOrderId galleryOrderId)
    {
        return type switch
        {
            DeliveryType.Digital => CreateDigital(price, quantity, productId, galleryOrderId),
            DeliveryType.Physical => CreatePhysical(price, quantity, productId, galleryOrderId),
            DeliveryType.Both => CreateDigitalAndPhysical(price, quantity, productId, galleryOrderId),
            _ => throw GalleryOrderValidationException.General(),
        };
    }

    public static GalleryOrderItem CreateDigital(Money price, int quantity, ProductId productId, GalleryOrderId galleryOrderId)
        => new GalleryOrderItem(DeliveryType.Digital, price, quantity, productId, galleryOrderId)
            .ValidateQuantity()
            .ValidatePriceAmount();

    public static GalleryOrderItem CreatePhysical(Money price, int quantity, ProductId productId, GalleryOrderId galleryOrderId)
        => new GalleryOrderItem(DeliveryType.Physical, price, quantity, productId, galleryOrderId)
            .ValidateQuantity()
            .ValidatePriceAmount();

    public static GalleryOrderItem CreateDigitalAndPhysical(Money price, int quantity, ProductId productId, GalleryOrderId galleryOrderId)
        => new GalleryOrderItem(DeliveryType.Both, price, quantity, productId, galleryOrderId)
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

    public GalleryOrderItem SetCadId(CadId cadId)
    {
        if (DeliveryType is DeliveryType.Digital or DeliveryType.Both)
        {
            CadId = cadId;
        }
        else throw GalleryOrderItemValidationException.CadIdOnNonDigitalDeliveryType();

        return this;
    }

    public GalleryOrderItem SetShipmentId(ShipmentId shipmentId)
    {
        if (DeliveryType is DeliveryType.Physical or DeliveryType.Both)
        {
            ShipmentId = shipmentId;
        }
        else throw GalleryOrderItemValidationException.ShipmentIdOnNonPhysicalDeliveryType();

        return this;
    }
}
