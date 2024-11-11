namespace CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Physical;

public record Address(string Country, string City, string Street)
{
    public Address() : this(string.Empty, string.Empty, string.Empty) { }
}
