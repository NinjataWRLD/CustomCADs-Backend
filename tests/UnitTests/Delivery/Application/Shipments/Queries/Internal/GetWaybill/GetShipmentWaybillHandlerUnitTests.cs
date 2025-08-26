using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetWaybill;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetWaybill;

using static Constants.Users;
using static ShipmentsData;

public class GetShipmentWaybillHandlerUnitTests : ShipmentsBaseUnitTests
{
	private readonly GetShipmentWaybillHandler handler;
	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IDeliveryService> delivery = new();
	private readonly Mock<BaseCachingService<ShipmentId, Shipment>> cache = new();

	private static readonly byte[] bytes = [1, 2, 3, 4, 5, 6];
	private static readonly AccountId headDesignerId = AccountId.New(DesignerAccountId);

	public GetShipmentWaybillHandlerUnitTests()
	{
		handler = new(reads.Object, delivery.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Shipment>>>()
		)).ReturnsAsync(CreateShipment());

		delivery.Setup(x => x.PrintAsync(ValidReferenceId, ct)).ReturnsAsync(bytes);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(ValidId, headDesignerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Shipment>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldCallDelivery()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(ValidId, headDesignerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		delivery.Verify(x => x.PrintAsync(ValidReferenceId, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(ValidId, headDesignerId);

		// Act
		byte[] result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(result, bytes);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCallerNotHeadDesigner()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(ValidId, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Shipment>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
