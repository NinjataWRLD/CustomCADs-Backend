namespace CustomCADs.UnitTests.Delivery.Domain.Shipments;

public class ShipmentsBaseUnitTests
{
    public const string ValidCountry1 = "Bulgaria";
    public const string ValidCountry2 = "Romania";
    public const string InvalidCountry = "";

    public const string ValidCity1 = "Sofia";
    public const string ValidCity2 = "Bucharest";
    public const string InvalidCity = "";
    
    public const string ValidReferenceId = "some-reference-id";
    public const string ValidBuyerId = "c5f5f3f3-3f3f-3f3f-3f3f-3f3f3f3f3f3f";

    protected static Shipment CreateShipment(string country = ValidCountry1, string city = ValidCity1, string referenceId = ValidReferenceId, string buyerId = ValidBuyerId)
        => Shipment.Create(new(country, city), referenceId, new(Guid.Parse(buyerId)));
}
