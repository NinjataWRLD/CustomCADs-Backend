using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetSortings;
using CustomCADs.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetSortings;

public class GetShipmentSortingsHandlerUnitTests : ShipmentsBaseUnitTests
{
	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetShipmentSortingsQuery query = new();
		GetShipmentSortingsHandler handler = new();

		// Act
		string[] sortings = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetNames<ShipmentSortingType>());
	}
}
