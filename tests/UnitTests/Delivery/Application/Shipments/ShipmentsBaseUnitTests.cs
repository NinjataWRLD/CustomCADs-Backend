using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments;

using static ShipmentsData;

public class ShipmentsBaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static Shipment CreateShipment(string country = ValidCountry, string city = ValidCity, string street = ValidStreet, string referenceId = ValidReferenceId, AccountId? buyerId = null)
		=> Shipment.Create(new(country, city, street), referenceId, buyerId ?? ValidBuyerId);

	protected static Shipment CreateShipmentWithId(ShipmentId? id = null, string country = ValidCountry, string city = ValidCity, string street = ValidStreet, string referenceId = ValidReferenceId, AccountId? buyerId = null)
		=> Shipment.CreateWithId(id ?? ValidId, new(country, city, street), referenceId, buyerId ?? ValidBuyerId);

	protected static ShipmentStatusDto[] CreateShipmentStatusDtos(int count = 4, string message = "Message")
		=> [..
			Enumerable.Range(1, count).Select(i => new ShipmentStatusDto(
				DateTime: DateTimeOffset.UtcNow.AddSeconds(i),
				Place: null,
				Message: message + i
			))
		];
}
