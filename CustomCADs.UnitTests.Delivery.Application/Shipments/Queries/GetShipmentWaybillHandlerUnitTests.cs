using CustomCADs.Delivery.Application.Shipments.Queries.GetWaybill;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries;

using static Constants.Users;

public class GetShipmentWaybillHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly IShipmentReads reads = Substitute.For<IShipmentReads>();
    private readonly IDeliveryService delivery = Substitute.For<IDeliveryService>();
    private static readonly Shipment Shipment = CreateShipment();
    private static readonly AccountId DesignerId = new(Guid.Parse(DesignerAccountId));
    private static readonly byte[] Bytes = [1, 2, 3, 4, 5, 6];

    public GetShipmentWaybillHandlerUnitTests()
    {
        reads.SingleByIdAsync(id, false, ct).Returns(Shipment);
        delivery.PrintAsync(ValidReferenceId, ct).Returns(Bytes);
    }

    [Fact]
    public async Task Handle_ShouldCallDatbase()
    {
        // Arrange
        GetShipmentWaybillQuery query = new(id, DesignerId);
        GetShipmentWaybillHandler handler = new(reads, delivery);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, false, ct);
    }

    [Fact]
    public async Task Handle_ShouldCallDelivery()
    {
        // Arrange
        GetShipmentWaybillQuery query = new(id, DesignerId);
        GetShipmentWaybillHandler handler = new(reads, delivery);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await delivery.Received(1).PrintAsync(ValidReferenceId, ct);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetShipmentWaybillQuery query = new(id, DesignerId);
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
        GetShipmentWaybillQuery query = new(id, DesignerId);
        GetShipmentWaybillHandler handler = new(reads, delivery);

        // Assert
        await Assert.ThrowsAsync<ShipmentNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCallerIsNotHeadDesigner()
    {
        // Arrange
        GetShipmentWaybillQuery query = new(id, new(Guid.Parse(ValidBuyerId)));
        GetShipmentWaybillHandler handler = new(reads, delivery);

        // Assert
        await Assert.ThrowsAsync<ShipmentAuthorizationException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
