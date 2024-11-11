namespace CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;

public record DigitalDelivery(Cad Cad)
{
    public DigitalDelivery() : this(Cad: new()) { }
}
