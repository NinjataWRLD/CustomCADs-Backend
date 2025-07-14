using CustomCADs.Delivery.Application.Shipments.Commands.Internal.Cancel;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Delivery;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Internal.Cancel;

using static ShipmentsData;

public class CancelShipmentHandlerUnitTests : ShipmentsBaseUnitTests
{
	private readonly CancelShipmentHandler handler;
	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IDeliveryService> delivery = new();

	private const string Comment = "Cancelling due to unpredicted travelling abroad";

	public CancelShipmentHandlerUnitTests()
	{
		handler = new(reads.Object, delivery.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateShipment(referenceId: ValidReferenceId));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CancelShipmentCommand command = new(ValidId, Comment);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCallDelivery_WhenShipmentFound()
	{
		// Arrange
		CancelShipmentCommand command = new(ValidId, Comment);

		// Act
		await handler.Handle(command, ct);

		// Assert
		delivery.Verify(x => x.CancelAsync(ValidReferenceId, Comment, ct), Times.Once());
	}
}
