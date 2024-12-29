using CustomCADs.Delivery.Application.Shipments.Queries.GetStatus;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries;

public class GetShipmentTrackHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly IShipmentReads reads = Substitute.For<IShipmentReads>();
    private readonly IDeliveryService delivery = Substitute.For<IDeliveryService>();
    private static readonly Shipment Shipment = CreateShipment();
    private static readonly ShipmentStatusDto[] Statuses = CreateShipmentStatusDto("Message");

    public GetShipmentTrackHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, false, ct).Returns(Shipment);
        delivery.TrackAsync(ShipmentValidReferenceId, ct).Returns(Statuses);
    }

    [Fact]
    public async Task Handle_ShouldCallDatbase()
    {
        // Arrange
        GetShipmentTrackQuery query = new(id);
        GetShipmentTrackHandler handler = new(reads, delivery);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, false, ct);
    }

    [Fact]
    public async Task Handle_ShouldCallDelivery()
    {
        // Arrange
        GetShipmentTrackQuery query = new(id);
        GetShipmentTrackHandler handler = new(reads, delivery);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await delivery.Received(1).TrackAsync(ShipmentValidReferenceId, ct);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetShipmentTrackQuery query = new(id);
        GetShipmentTrackHandler handler = new(reads, delivery);

        // Act
        Dictionary<DateTime, GetShipmentTrackDto> tracks = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(tracks, Statuses.ToDictionary(x => x.DateTime, x => new GetShipmentTrackDto(x.Message, x.Place)));
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenShipmentNotFound()
    {
        // Arrange
        reads.SingleByIdAsync(id, false, ct).Returns(null as Shipment);
        GetShipmentTrackQuery query = new(id);
        GetShipmentTrackHandler handler = new(reads, delivery);

        // Assert
        await Assert.ThrowsAsync<ShipmentNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }

    private static ShipmentStatusDto[] CreateShipmentStatusDto(string message)
        => [..
            Enumerable.Range(1, 3).Select(i => new ShipmentStatusDto(
                DateTime: DateTime.UtcNow.AddSeconds(i),
                Place: null,
                Message: message + i
            ))
        ];
}
