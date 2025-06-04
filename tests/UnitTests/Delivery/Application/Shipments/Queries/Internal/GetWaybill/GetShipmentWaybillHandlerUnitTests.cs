using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetWaybill;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Delivery;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetWaybill;

using static Constants.Users;
using static ShipmentsData;

public class GetShipmentWaybillHandlerUnitTests : ShipmentsBaseUnitTests
{
	private readonly GetShipmentWaybillHandler handler;
	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IDeliveryService> delivery = new();

	private static readonly byte[] bytes = [1, 2, 3, 4, 5, 6];
	public static readonly AccountId headDesignerId = AccountId.New(DesignerAccountId);

	public GetShipmentWaybillHandlerUnitTests()
	{
		handler = new(reads.Object, delivery.Object);

		reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(CreateShipment());
		delivery.Setup(x => x.PrintAsync(ValidReferenceId, ct)).ReturnsAsync(bytes);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatbase()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(id, headDesignerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldCallDelivery_WhenShipmentFound()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(id, headDesignerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		delivery.Verify(x => x.PrintAsync(ValidReferenceId, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly_WhenShipmentFound()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(id, headDesignerId);

		// Act
		byte[] bytes = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(bytes, GetShipmentWaybillHandlerUnitTests.bytes);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenShipmentNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(null as Shipment);
		GetShipmentWaybillQuery query = new(id, headDesignerId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCallerNotHeadDesigner()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(id, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Shipment>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
