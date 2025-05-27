using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipmnet;

using static ActiveCartsData;

public class CalculateActiveCartShipmentHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private const string TimeZone = "Europe/Sofia";
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();
	private static readonly AccountId buyerId = ValidBuyerId1;
	private static readonly AddressDto address = new("Bulgaria", "Burgas");

	public CalculateActiveCartShipmentHandlerUnitTests()
	{
		reads.Setup(x => x.AllAsync(buyerId, false, ct))
			.ReturnsAsync([
				CreateItem(ValidBuyerId1, ValidProductId1),
				CreateItemWithDelivery(ValidBuyerId2, ValidProductId2),
			]);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCustomizationsWeightByIdsQuery>(), ct))
			.ReturnsAsync([]);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<CalculateShipmentQuery>(), ct))
			.ReturnsAsync([]);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CalculateActiveCartShipmentQuery query = new(buyerId, address);
		CalculateActiveCartShipmentHandler handler = new(reads.Object, sender.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(buyerId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CalculateActiveCartShipmentQuery query = new(buyerId, address);
		CalculateActiveCartShipmentHandler handler = new(reads.Object, sender.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetCustomizationsWeightByIdsQuery>(),
		ct), Times.Once);
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<CalculateShipmentQuery>(),
		ct), Times.Once);
	}
}
