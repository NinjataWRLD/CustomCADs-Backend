namespace CustomCADs.Orders.Domain.OrderDeliveries.ValueObjects;

public record Address(string Country, string City, string Street)
{
    public Address() : this(string.Empty, string.Empty, string.Empty) { }
}