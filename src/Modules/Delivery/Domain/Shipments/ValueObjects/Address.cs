namespace CustomCADs.Delivery.Domain.Shipments.ValueObjects;

public record Address
{
	public Address() { }
	public Address(string country, string city) : this()
	{
		Country = country ?? throw new ArgumentNullException(nameof(country));
		City = city ?? throw new ArgumentNullException(nameof(country));
	}

	public string Country { get; set; } = string.Empty;
	public string City { get; set; } = string.Empty;
}
