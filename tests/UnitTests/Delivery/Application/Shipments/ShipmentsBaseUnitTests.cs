﻿using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments;

using static ShipmentsData;

public class ShipmentsBaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static Shipment CreateShipment(string country = ValidCountry, string city = ValidCity, string referenceId = ValidReferenceId, AccountId? buyerId = null)
		=> Shipment.Create(new(country, city), referenceId, buyerId ?? ValidBuyerId);

	protected static ShipmentStatusDto[] CreateShipmentStatusDtos(int count = 4, string message = "Message")
		=> [..
			Enumerable.Range(1, count).Select(i => new ShipmentStatusDto(
				DateTime: DateTimeOffset.UtcNow.AddSeconds(i),
				Place: null,
				Message: message + i
			))
		];
}
