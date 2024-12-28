using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments;

public class ShipmentsBaseUnitTests
{
    public const string ValidCountry1 = "Bulgaria";
    public const string ValidCountry2 = "Romania";
    public const string InvalidCountry = "";

    public const string ValidCity1 = "Sofia";
    public const string ValidCity2 = "Bucharest";
    public const string InvalidCity = "";

    public const string? ValidPhone1 = "1234567890";
    public const string? ValidPhone2 = null;
    
    public const string? ValidEmail1 = "john.doe@example.com";
    public const string? ValidEmail2 = null;
    
    public const string ValidService1 = "Standard";
    public const string ValidService2 = "Express";

    public const int ValidCount1 = 1;
    public const int ValidCount2 = 2;
    
    public const double ValidWeight1 = 1.5;
    public const double ValidWeight2 = 2.5;

    public const string ValidRecipient1 = "John Doe";
    public const string ValidRecipient2 = "Jane Doe";

    public const string ValidReferenceId = "some-reference-id";
    public const string ValidBuyerId = "c5f5f3f3-3f3f-3f3f-3f3f-3f3f3f3f3f3f";
    
    protected static readonly ShipmentId id = new(Guid.Parse("00000000-0000-0000-0000-000000000001"));
    protected static readonly CancellationToken ct = CancellationToken.None;

    protected static Shipment CreateShipment(string country = ValidCountry1, string city = ValidCity1, string referenceId = ValidReferenceId, string buyerId = ValidBuyerId)
        => Shipment.Create(new(country, city), referenceId, new(Guid.Parse(buyerId)));
}
