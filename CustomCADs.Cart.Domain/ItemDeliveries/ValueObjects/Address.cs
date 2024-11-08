namespace CustomCADs.Cart.Domain.ItemDeliveries.ValueObjects;

public record Address(string Country, string City, string Street)
{
    public Address() : this(string.Empty, string.Empty, string.Empty) { }
}