using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetStatus;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetTrack;

using static ShipmentsData;

public class GetShipmentTrackHandlerUnitTests : ShipmentsBaseUnitTests
{
	private readonly GetShipmentTrackHandler handler;
	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IDeliveryService> delivery = new();
	private readonly Mock<BaseCachingService<ShipmentId, Shipment>> cache = new();

	private static readonly ShipmentStatusDto[] statuses = CreateShipmentStatusDtos();

	public GetShipmentTrackHandlerUnitTests()
	{
		handler = new(reads.Object, delivery.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Shipment>>>()
		)).ReturnsAsync(CreateShipment());

		delivery.Setup(x => x.TrackAsync(ValidReferenceId, ct)).ReturnsAsync(statuses);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetShipmentTrackQuery query = new(ValidId);

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
		GetShipmentTrackQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		delivery.Verify(x => x.TrackAsync(ValidReferenceId, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetShipmentTrackQuery query = new(ValidId);

		// Act
		Dictionary<DateTimeOffset, GetShipmentTrackDto> tracks = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(tracks, statuses.ToDictionary(x => x.DateTime, x => new GetShipmentTrackDto(x.Message, x.Place)));
	}
}
