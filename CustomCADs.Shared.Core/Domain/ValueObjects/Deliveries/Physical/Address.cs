namespace CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Physical;

public record Address
{
    public Address() { }
    public Address(string country, string city, string street) : this()
    {
        Country = country ?? throw new ArgumentNullException(nameof(country));
        City = city ?? throw new ArgumentNullException(nameof(country));
        Street = street ?? throw new ArgumentNullException(nameof(country));
    }
    
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
}
