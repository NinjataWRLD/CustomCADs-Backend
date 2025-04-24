using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetAll;

using static ShipmentsData;

public class GetAllShipmentsHandlerUnitTests : ShipmentsBaseUnitTests
{
    private readonly GetAllShipmentsHandler handler;
    private readonly Mock<IShipmentReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private static readonly Dictionary<AccountId, string> buyers = new()
    {
        [ValidBuyerId] = "NinjataBG"
    };
    private static readonly Shipment[] shipments = [
        Shipment.Create(new(ValidCountry1, ValidCity1), ValidReferenceId, ValidBuyerId),
        Shipment.Create(new(ValidCountry2, ValidCity1), ValidReferenceId, ValidBuyerId),
        Shipment.Create(new(ValidCountry1, ValidCity2), ValidReferenceId, ValidBuyerId),
        Shipment.Create(new(ValidCountry2, ValidCity2), ValidReferenceId, ValidBuyerId),
    ];
    private readonly ShipmentQuery shipmentQuery = new(new(), null, null);

    public GetAllShipmentsHandlerUnitTests()
    {
        handler = new(reads.Object, sender.Object);

        reads.Setup(x => x.AllAsync(
            It.IsAny<ShipmentQuery>(),
            false,
            ct
        )).ReturnsAsync(new Result<Shipment>(shipments.Length, shipments));

        sender.Setup(x => x.SendQueryAsync(
            It.Is<GetUsernamesByIdsQuery>(x => x.Ids.Length == shipments.Length),
            ct
        )).ReturnsAsync(buyers);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetAllShipmentsQuery query = new(
            Pagination: new(),
            CustomerId: null,
            Sorting: null
        );

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.AllAsync(shipmentQuery, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetAllShipmentsQuery query = new(
            Pagination: new(),
            CustomerId: null,
            Sorting: null
        );

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetUsernamesByIdsQuery>(x => x.Ids.Length == shipments.Length),
            ct
        ), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Assert
        GetAllShipmentsQuery query = new(
            Pagination: new(),
            CustomerId: null,
            Sorting: null
        );

        // Act
        Result<GetAllShipmentsDto> result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(result.Items.Select(r => r.Address), shipments.Select(r => r.Address)),
            () => Assert.Equal(result.Items.Select(r => r.BuyerName), shipments.Select(r => buyers[r.BuyerId]))
        );
    }
}
