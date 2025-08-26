using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Application.Shipments.Commands.Internal.Cancel;
using CustomCADs.Delivery.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Internal.Cancel;

using static ShipmentsData;

public class CancelShipmentHandlerUnitTests : ShipmentsBaseUnitTests
{
	private readonly CancelShipmentHandler handler;
	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IDeliveryService> delivery = new();
	private readonly Mock<BaseCachingService<ShipmentId, Shipment>> cache = new();

	private const string Comment = "Cancelling due to unpredicted travelling abroad";

	public CancelShipmentHandlerUnitTests()
	{
		handler = new(reads.Object, delivery.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Shipment>>>()
		)).ReturnsAsync(CreateShipment(referenceId: ValidReferenceId));
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		CancelShipmentCommand command = new(ValidId, Comment);

		// Act
		await handler.Handle(command, ct);

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
		CancelShipmentCommand command = new(ValidId, Comment);

		// Act
		await handler.Handle(command, ct);

		// Assert
		delivery.Verify(x => x.CancelAsync(ValidReferenceId, Comment, ct), Times.Once());
	}
}
