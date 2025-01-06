using CustomCADs.Delivery.Application.Shipments.Queries.GetAll;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.GetAll;

using static ShipmentsData;

public class GetAllShipmentsHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly Mock<IShipmentReads> reads = new();
    private static readonly Shipment[] Shipments = [
        Shipment.Create(new(ValidCountry1, ValidCity1), ValidReferenceId, ValidBuyerId),
        Shipment.Create(new(ValidCountry2, ValidCity1), ValidReferenceId, ValidBuyerId),
        Shipment.Create(new(ValidCountry1, ValidCity2), ValidReferenceId, ValidBuyerId),
        Shipment.Create(new(ValidCountry2, ValidCity2), ValidReferenceId, ValidBuyerId),
    ];
    private readonly ShipmentQuery shipmentQuery = new(new(), null, null);

    public GetAllShipmentsHandlerUnitTests()
    {
        Result<Shipment> result = new(Shipments.Length, Shipments);
        reads.Setup(x => x.AllAsync(It.IsAny<ShipmentQuery>(), false, ct))
            .ReturnsAsync(result);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetAllShipmentsQuery query = new(
            Pagination: new(),
            ClientId: null,
            Sorting: null
        );
        GetAllShipmentsHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.AllAsync(shipmentQuery, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Assert
        GetAllShipmentsQuery query = new(
            Pagination: new(),
            ClientId: null,
            Sorting: null
        );
        GetAllShipmentsHandler handler = new(reads.Object);

        // Act
        Result<GetAllShipmentsDto> result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(result.Items.Select(r => r.Address), Shipments.Select(r => r.Address)),
            () => Assert.Equal(result.Items.Select(r => r.BuyerId), Shipments.Select(r => r.BuyerId))
        );
    }
}
