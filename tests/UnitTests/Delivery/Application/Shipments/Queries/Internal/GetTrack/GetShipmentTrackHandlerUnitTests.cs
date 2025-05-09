﻿using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetStatus;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Delivery;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetTrack;

using static ShipmentsData;

public class GetShipmentTrackHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly Mock<IShipmentReads> reads = new();
    private readonly Mock<IDeliveryService> delivery = new();
    private static readonly ShipmentStatusDto[] Statuses = CreateShipmentStatusDtos(4, "Message");

    public GetShipmentTrackHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(CreateShipment());
        delivery.Setup(x => x.TrackAsync(ValidReferenceId, ct)).ReturnsAsync(Statuses);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatbase()
    {
        // Arrange
        GetShipmentTrackQuery query = new(id);
        GetShipmentTrackHandler handler = new(reads.Object, delivery.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallDelivery_WhenShipmentFound()
    {
        // Arrange
        GetShipmentTrackQuery query = new(id);
        GetShipmentTrackHandler handler = new(reads.Object, delivery.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        delivery.Verify(x => x.TrackAsync(ValidReferenceId, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly_WhenShipmentFound()
    {
        // Arrange
        GetShipmentTrackQuery query = new(id);
        GetShipmentTrackHandler handler = new(reads.Object, delivery.Object);

        // Act
        Dictionary<DateTimeOffset, GetShipmentTrackDto> tracks = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(tracks, Statuses.ToDictionary(x => x.DateTime, x => new GetShipmentTrackDto(x.Message, x.Place)));
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenShipmentNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(null as Shipment);

        GetShipmentTrackQuery query = new(id);
        GetShipmentTrackHandler handler = new(reads.Object, delivery.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }

    private static ShipmentStatusDto[] CreateShipmentStatusDtos(int count, string message)
        => [..
            Enumerable.Range(1, count).Select(i => new ShipmentStatusDto(
                DateTime: DateTimeOffset.UtcNow.AddSeconds(i),
                Place: null,
                Message: message + i
            ))
        ];
}
