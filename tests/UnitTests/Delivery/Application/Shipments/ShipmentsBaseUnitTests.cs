using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments;

using static ShipmentsData;

public class ShipmentsBaseUnitTests
{
	protected static readonly ShipmentId id = ShipmentId.New();
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static Shipment CreateShipment(string country = ValidCountry1, string city = ValidCity1, string referenceId = ValidReferenceId, AccountId? buyerId = null)
		=> Shipment.Create(new(country, city), referenceId, buyerId ?? ValidBuyerId);
}
