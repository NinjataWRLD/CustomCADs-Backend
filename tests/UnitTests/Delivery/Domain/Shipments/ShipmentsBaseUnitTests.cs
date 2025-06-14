using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments;

using static ShipmentsData;

public class ShipmentsBaseUnitTests
{
	public static Shipment CreateShipment(string? country = null, string? city = null, string? referenceId = null, AccountId? buyerId = null)
		=> Shipment.Create(new(country ?? ValidCountry, city ?? ValidCity), referenceId ?? ValidReferenceId, buyerId ?? ValidBuyerId);

	public static Shipment CreateShipmentWithId(ShipmentId? id = null, string? country = null, string? city = null, string? referenceId = null, AccountId? buyerId = null)
		=> Shipment.CreateWithId(id ?? ValidId, new(country ?? ValidCountry, city ?? ValidCity), referenceId ?? ValidReferenceId, buyerId ?? ValidBuyerId);
}
