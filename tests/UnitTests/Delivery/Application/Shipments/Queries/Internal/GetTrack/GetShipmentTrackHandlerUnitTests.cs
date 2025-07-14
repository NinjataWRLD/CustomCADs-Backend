using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetStatus;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Delivery;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetTrack;

using static ShipmentsData;

public class GetShipmentTrackHandlerUnitTests : ShipmentsBaseUnitTests
{
	private readonly GetShipmentTrackHandler handler;
	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IDeliveryService> delivery = new();

	private static readonly ShipmentStatusDto[] statuses = CreateShipmentStatusDtos();

	public GetShipmentTrackHandlerUnitTests()
	{
		handler = new(reads.Object, delivery.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateShipment());
		delivery.Setup(x => x.TrackAsync(ValidReferenceId, ct)).ReturnsAsync(statuses);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetShipmentTrackQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCallDelivery_WhenShipmentFound()
	{
		// Arrange
		GetShipmentTrackQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		delivery.Verify(x => x.TrackAsync(ValidReferenceId, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult_WhenShipmentFound()
	{
		// Arrange
		GetShipmentTrackQuery query = new(ValidId);

		// Act
		Dictionary<DateTimeOffset, GetShipmentTrackDto> tracks = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(tracks, statuses.ToDictionary(x => x.DateTime, x => new GetShipmentTrackDto(x.Message, x.Place)));
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenShipmentNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Shipment);
		GetShipmentTrackQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
