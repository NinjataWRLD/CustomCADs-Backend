using CustomCADs.Delivery.Application.Shipments.Queries.GetAll;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries;

public class GetAllShipmentsHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly IShipmentReads reads = Substitute.For<IShipmentReads>();
    private static readonly Shipment[] Shipments = [
        Shipment.Create(new(ShipmentValidCountry1, ShipmentValidCity1), ShipmentValidReferenceId, new(Guid.Parse(ShipmentValidBuyerId))),
        Shipment.Create(new(ShipmentValidCountry1, ShipmentValidCity2), ShipmentValidReferenceId, new(Guid.Parse(ShipmentValidBuyerId))),
        Shipment.Create(new(ShipmentValidCountry1, ShipmentValidCity2), ShipmentValidReferenceId, new(Guid.Parse(ShipmentValidBuyerId))),
        Shipment.Create(new(ShipmentValidCountry2, ShipmentValidCity2), ShipmentValidReferenceId, new(Guid.Parse(ShipmentValidBuyerId))),
    ];
    private readonly ShipmentQuery shipmentQuery = new(new(), null, null);

    public GetAllShipmentsHandlerUnitTests()
    {
        Result<Shipment> result = new(Shipments.Length, Shipments);
        reads.AllAsync(shipmentQuery, track: false, ct).Returns(result);
    }

    [Fact]
    public async Task Handle_ShouldCallDatabase()
    {
        // Arrange
        GetAllShipmentsQuery query = new(
            Pagination: new(),
            ClientId: null,
            Sorting: null
        );
        GetAllShipmentsHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).AllAsync(shipmentQuery, track: false, ct);
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
        GetAllShipmentsHandler handler = new(reads);

        // Act
        Result<GetAllShipmentsDto> result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(result.Items.Select(r => r.Address), Shipments.Select(r => r.Address)),
            () => Assert.Equal(result.Items.Select(r => r.BuyerId), Shipments.Select(r => r.BuyerId))
        );
    }
}
