using CustomCADs.Delivery.Application.Shipments.Commands.Cancel;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Cancel;

using static ShipmentsData;

public class CancelShipmentHandlerData : TheoryData<string, int, double, string, string, string, string?, string?>;

public class CancelShipmentHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly IShipmentReads reads = Substitute.For<IShipmentReads>();
    private readonly IDeliveryService delivery = Substitute.For<IDeliveryService>();
    private const string comment = "Cancelling due to unpredicted travelling abroad";

    public CancelShipmentHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, false, ct)
            .Returns(CreateShipment(referenceId: ValidReferenceId));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        CancelShipmentCommand command = new(
            Id: id,
            Comment: comment
        );
        CancelShipmentHandler handler = new(reads, delivery);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, false, ct);
    }

    [Fact]
    public async Task Handle_ShouldCallDelivery_WhenShipmentFound()
    {
        // Arrange
        CancelShipmentCommand command = new(
            Id: id,
            Comment: comment
        );
        CancelShipmentHandler handler = new(reads, delivery);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await delivery.Received(1).CancelAsync(ValidReferenceId, comment, ct);
    }
}
