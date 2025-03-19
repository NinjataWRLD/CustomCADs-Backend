using CustomCADs.Delivery.Application.Shipments.Commands.Cancel;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Delivery;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Cancel;

using static ShipmentsData;

public class CancelShipmentHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly Mock<IShipmentReads> reads = new();
    private readonly Mock<IDeliveryService> delivery = new();
    private const string comment = "Cancelling due to unpredicted travelling abroad";

    public CancelShipmentHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(CreateShipment(referenceId: ValidReferenceId));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        CancelShipmentCommand command = new(
            Id: id,
            Comment: comment
        );
        CancelShipmentHandler handler = new(reads.Object, delivery.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallDelivery_WhenShipmentFound()
    {
        // Arrange
        CancelShipmentCommand command = new(
            Id: id,
            Comment: comment
        );
        CancelShipmentHandler handler = new(reads.Object, delivery.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        delivery.Verify(x => x.CancelAsync(ValidReferenceId, comment, ct), Times.Once);
    }
}
