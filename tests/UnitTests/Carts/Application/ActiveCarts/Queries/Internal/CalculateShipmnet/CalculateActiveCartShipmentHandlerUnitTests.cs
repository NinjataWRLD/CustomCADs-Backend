using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet;

using static ActiveCartsData;

public class CalculateActiveCartShipmentHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private readonly CalculateActiveCartShipmentHandler handler;
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly AddressDto address = new("Bulgaria", "Burgas", "Slivnitsa");

	public CalculateActiveCartShipmentHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.AllAsync(ValidBuyerId, false, ct))
			.ReturnsAsync([
				CreateItem(ValidBuyerId, ValidProductId),
				CreateItemWithDelivery(ValidBuyerId, ValidProductId),
			]);

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<GetCustomizationsWeightByIdsQuery>(),
			ct
		)).ReturnsAsync([]);

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<CalculateShipmentQuery>(),
			ct
		)).ReturnsAsync([]);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CalculateActiveCartShipmentQuery query = new(ValidBuyerId, address);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(ValidBuyerId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CalculateActiveCartShipmentQuery query = new(ValidBuyerId, address);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetCustomizationsWeightByIdsQuery>(),
			ct
		), Times.Once);
		sender.Verify(x => x.SendQueryAsync(
			It.Is<CalculateShipmentQuery>(x => x.Address == address),
			ct
		), Times.Once);
	}
}
