using CustomCADs.Delivery.Application.Shipments.Queries.GetWaybill;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.Application.Delivery;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.GetWaybill;

using static ShipmentsData;

public class GetShipmentWaybillHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly Mock<IShipmentReads> reads = new();
    private readonly Mock<IDeliveryService> delivery = new();
    private static readonly byte[] Bytes = [1, 2, 3, 4, 5, 6];

    public GetShipmentWaybillHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(CreateShipment());
        delivery.Setup(x => x.PrintAsync(ValidReferenceId, ct)).ReturnsAsync(Bytes);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatbase()
    {
        // Arrange
        GetShipmentWaybillQuery query = new(id, ValidHeadDesignerId);
        GetShipmentWaybillHandler handler = new(reads.Object, delivery.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallDelivery_WhenShipmentFound()
    {
        // Arrange
        GetShipmentWaybillQuery query = new(id, ValidHeadDesignerId);
        GetShipmentWaybillHandler handler = new(reads.Object, delivery.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        delivery.Verify(x => x.PrintAsync(ValidReferenceId, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly_WhenShipmentFound()
    {
        // Arrange
        GetShipmentWaybillQuery query = new(id, ValidHeadDesignerId);
        GetShipmentWaybillHandler handler = new(reads.Object, delivery.Object);

        // Act
        byte[] bytes = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(bytes, Bytes);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenShipmentNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(null as Shipment);
        GetShipmentWaybillQuery query = new(id, ValidHeadDesignerId);
        GetShipmentWaybillHandler handler = new(reads.Object, delivery.Object);

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
        GetShipmentWaybillHandler handler = new(reads.Object, delivery.Object);

        // Assert
        await Assert.ThrowsAsync<ShipmentAuthorizationException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
