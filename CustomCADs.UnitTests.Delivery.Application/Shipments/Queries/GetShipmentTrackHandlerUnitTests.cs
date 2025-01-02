using CustomCADs.Delivery.Application.Shipments.Queries.GetStatus;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries;

using static ShipmentsData;

public class GetShipmentTrackHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly IShipmentReads reads = Substitute.For<IShipmentReads>();
    private readonly IDeliveryService delivery = Substitute.For<IDeliveryService>();
    private static readonly ShipmentStatusDto[] Statuses = CreateShipmentStatusDtos(4, "Message");

    public GetShipmentTrackHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, false, ct).Returns(CreateShipment());
        delivery.TrackAsync(ValidReferenceId, ct).Returns(Statuses);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatbase()
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
    public async Task Handle_ShouldCallDelivery_WhenShipmentFound()
    {
        // Arrange
        GetShipmentTrackQuery query = new(id);
        GetShipmentTrackHandler handler = new(reads, delivery);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await delivery.Received(1).TrackAsync(ValidReferenceId, ct);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly_WhenShipmentFound()
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

    private static ShipmentStatusDto[] CreateShipmentStatusDtos(int count, string message)
        => [..
            Enumerable.Range(1, count).Select(i => new ShipmentStatusDto(
                DateTime: DateTime.UtcNow.AddSeconds(i),
                Place: null,
                Message: message + i
            ))
        ];
}
