using CustomCADs.Orders.Domain.Carts.Validation;
using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.Common.Exceptions.Carts;
using CustomCADs.Orders.Domain.Common.Exceptions.Carts.GalleryOrders;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Shipments;

namespace CustomCADs.Orders.Domain.Carts.Entities;

public class GalleryOrder : BaseEntity
{
    private GalleryOrder() { }
    private GalleryOrder(DeliveryType deliveryType, Money price, int quantity, ProductId productId, CartId galleryOrderId) : this()
    {
        Price = price;
        Quantity = quantity;
        DeliveryType = deliveryType;
        PurchaseDate = DateTime.UtcNow;
        ProductId = productId;
        CartId = galleryOrderId;
    }

    public GalleryOrderId Id { get; init; }
    public int Quantity { get; private set; }
    public DeliveryType DeliveryType { get; }
    public Money Price { get; private set; } = new();
    public DateTime PurchaseDate { get; }
    public ProductId ProductId { get; }
    public CartId CartId { get; }
    public Cart Cart { get; } = null!;
    public CadId? CadId { get; private set; }
    public ShipmentId? ShipmentId { get; private set; }
    public Money Cost => Price.Multiply(Quantity);

    public static GalleryOrder Create(DeliveryType type, Money price, int quantity, ProductId productId, CartId galleryOrderId)
    {
        return type switch
        {
            DeliveryType.Digital => CreateDigital(price, quantity, productId, galleryOrderId),
            DeliveryType.Physical => CreatePhysical(price, quantity, productId, galleryOrderId),
            DeliveryType.Both => CreateDigitalAndPhysical(price, quantity, productId, galleryOrderId),
            _ => throw CartValidationException.General(),
        };
    }

    public static GalleryOrder CreateDigital(Money price, int quantity, ProductId productId, CartId galleryOrderId)
        => new GalleryOrder(DeliveryType.Digital, price, quantity, productId, galleryOrderId)
            .ValidateQuantity()
            .ValidatePriceAmount();

    public static GalleryOrder CreatePhysical(Money price, int quantity, ProductId productId, CartId galleryOrderId)
        => new GalleryOrder(DeliveryType.Physical, price, quantity, productId, galleryOrderId)
            .ValidateQuantity()
            .ValidatePriceAmount();

    public static GalleryOrder CreateDigitalAndPhysical(Money price, int quantity, ProductId productId, CartId galleryOrderId)
        => new GalleryOrder(DeliveryType.Both, price, quantity, productId, galleryOrderId)
            .ValidateQuantity()
            .ValidatePriceAmount();

    public GalleryOrder SetQuantity(int quantity)
    {
        Quantity = quantity;
        this.ValidateQuantity();
        return this;
    }

    public GalleryOrder SetPrice(Money price)
    {
        Price = price;
        this.ValidatePriceAmount();
        return this;
    }

    public GalleryOrder SetCadId(CadId cadId)
    {
        if (DeliveryType is DeliveryType.Digital or DeliveryType.Both)
        {
            CadId = cadId;
        }
        else throw GalleryOrderValidationException.CadIdOnNonDigitalDeliveryType();

        return this;
    }

    public GalleryOrder SetShipmentId(ShipmentId shipmentId)
    {
        if (DeliveryType is DeliveryType.Physical or DeliveryType.Both)
        {
            ShipmentId = shipmentId;
        }
        else throw GalleryOrderValidationException.ShipmentIdOnNonPhysicalDeliveryType();

        return this;
    }
}
