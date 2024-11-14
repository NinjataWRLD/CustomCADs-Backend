using CustomCADs.Shared.Core.Domain.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;
using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Physical;

namespace CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries;

public record Delivery
{
    private const string PhysicalError = "Physical delivery info must be provided for physical delivery type";
    private const string DigitalError = "Digital delivery info must be provided for digital delivery type";
    private const string BothError = "Physical and Digital delivery info must be provided for physical and digital type";

    private Delivery() { }
    private Delivery(
        DeliveryType deliveryType,
        DigitalDelivery? digitalDelivery,
        PhysicalDelivery? physicalDelivery
    )
    {
        if (deliveryType == DeliveryType.Physical
            && physicalDelivery is null)
        {
            throw new ArgumentException(PhysicalError, nameof(deliveryType));
        }

        if (deliveryType == DeliveryType.Digital
            && digitalDelivery is null)
        {
            throw new ArgumentException(DigitalError, nameof(deliveryType));
        }

        if (deliveryType == DeliveryType.Both
            && (digitalDelivery is null || physicalDelivery is null))
        {
            throw new ArgumentException(BothError, nameof(deliveryType));
        }

        DeliveryType = deliveryType;
        DigitalDelivery = digitalDelivery;
        PhysicalDelivery = physicalDelivery;
    }

    public DeliveryType DeliveryType { get; init; }
    public DigitalDelivery? DigitalDelivery { get; init; }
    public PhysicalDelivery? PhysicalDelivery { get; init; }

    public static Delivery CreateNone() => new();

    public static Delivery CreateDigital(Cad cad)
    {
        return new(DeliveryType.Digital, new(cad), null);
    }

    public static Delivery CreatePhysical(DeliveryStatus status, Address address)
    {
        return new(DeliveryType.Physical, null, new(status, address));
    }

    public static Delivery CreateDigitalAndPhysical(Cad cad, DeliveryStatus status, Address address)
    {
        return new(DeliveryType.Both, new(cad), new(status, address));
    }
}
