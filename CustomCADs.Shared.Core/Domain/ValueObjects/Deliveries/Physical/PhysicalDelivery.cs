using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Physical;

public record PhysicalDelivery(DeliveryStatus Status, Address Address)
{
    public PhysicalDelivery() : this(DeliveryStatus.Pending, new()) { }
}
