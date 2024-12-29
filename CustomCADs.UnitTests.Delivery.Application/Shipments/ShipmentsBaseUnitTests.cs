using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments;

public class ShipmentsBaseUnitTests
{
    protected static readonly ShipmentId id = new(Guid.Parse("00000000-0000-0000-0000-000000000001"));
    protected static readonly CancellationToken ct = CancellationToken.None;

    protected static Shipment CreateShipment(string country = ShipmentValidCountry1, string city = ShipmentValidCity1, string referenceId = ShipmentValidReferenceId, string buyerId = ShipmentValidBuyerId)
        => Shipment.Create(new(country, city), referenceId, new(Guid.Parse(buyerId)));
}
