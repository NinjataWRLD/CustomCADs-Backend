using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments;

using static ShipmentsData;

public class ShipmentsBaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static Shipment CreateShipment(string country = ValidCountry, string city = ValidCity, string referenceId = ValidReferenceId, AccountId? buyerId = null)
		=> Shipment.Create(new(country, city), referenceId, buyerId ?? ValidBuyerId);
}
