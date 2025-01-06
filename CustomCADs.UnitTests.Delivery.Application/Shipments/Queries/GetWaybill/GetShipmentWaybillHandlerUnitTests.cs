using CustomCADs.Delivery.Application.Shipments.Queries.GetWaybill;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.Application.Delivery;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.GetWaybill;

using static ShipmentsData;

public class GetShipmentWaybillHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly IShipmentReads reads = Substitute.For<IShipmentReads>();
    private readonly IDeliveryService delivery = Substitute.For<IDeliveryService>();
    private static readonly byte[] Bytes = [1, 2, 3, 4, 5, 6];

    public GetShipmentWaybillHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, false, ct).Returns(CreateShipment());
        delivery.PrintAsync(ValidReferenceId, ct).Returns(Bytes);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatbase()
    {
        // Arrange
        GetShipmentWaybillQuery query = new(id, ValidHeadDesignerId);
        GetShipmentWaybillHandler handler = new(reads, delivery);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, false, ct);
    }

    [Fact]
    public async Task Handle_ShouldCallDelivery_WhenShipmentFound()
    {
        // Arrange
        GetShipmentWaybillQuery query = new(id, ValidHeadDesignerId);
        GetShipmentWaybillHandler handler = new(reads, delivery);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await delivery.Received(1).PrintAsync(ValidReferenceId, ct);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly_WhenShipmentFound()
    {
        // Arrange
        GetShipmentWaybillQuery query = new(id, ValidHeadDesignerId);
        GetShipmentWaybillHandler handler = new(reads, delivery);

        // Act
        byte[] bytes = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(bytes, Bytes);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenShipmentNotFound()
    {
        // Arrange
        reads.SingleByIdAsync(id, false, ct).Returns(null as Shipment);
        GetShipmentWaybillQuery query = new(id, ValidHeadDesignerId);
        GetShipmentWaybillHandler handler = new(reads, delivery);

        // Assert
        await Assert.ThrowsAsync<ShipmentNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCallerNotHeadDesigner()
    {
        // Arrange
        GetShipmentWaybillQuery query = new(id, ValidBuyerId);
        GetShipmentWaybillHandler handler = new(reads, delivery);

        // Assert
        await Assert.ThrowsAsync<ShipmentAuthorizationException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
