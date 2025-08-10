using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments;

using static ShipmentsData;

public class ShipmentsBaseUnitTests
{
	public static Shipment CreateShipment(string? country = null, string? city = null, string? street = null, string? referenceId = null, AccountId? buyerId = null)
		=> Shipment.Create(new(country ?? ValidCountry, city ?? ValidCity, street ?? ValidStreet), referenceId ?? ValidReferenceId, buyerId ?? ValidBuyerId);

	public static Shipment CreateShipmentWithId(ShipmentId? id = null, string? country = null, string? city = null, string? street = null, string? referenceId = null, AccountId? buyerId = null)
		=> Shipment.CreateWithId(id ?? ValidId, new(country ?? ValidCountry, city ?? ValidCity, street ?? ValidStreet), referenceId ?? ValidReferenceId, buyerId ?? ValidBuyerId);
}
