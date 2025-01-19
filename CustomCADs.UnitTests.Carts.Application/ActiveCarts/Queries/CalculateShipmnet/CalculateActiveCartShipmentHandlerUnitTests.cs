using CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.CalculateShipmnet;

using static ActiveCartsData;

public class CalculateActiveCartShipmentHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private const string TimeZone = "Europe/Sofia";
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly ActiveCartItemId itemId = ActiveCartItemId.New(Guid.Empty);
    private static readonly AddressDto address = new("Bulgaria", "Burgas");
    private static readonly ActiveCart cart = CreateCartWithItems(
        buyerId: buyerId,
        items: [
            CreateItem(ValidId1, CartItemsData.ValidProductId1, CartItemsData.ValidWeight1, true),
            CreateItem(ValidId2, CartItemsData.ValidProductId2, CartItemsData.ValidWeight2, false),
        ]
    );

    public CalculateActiveCartShipmentHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, false, ct))
            .ReturnsAsync(cart);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<CalculateShipmentQuery>(), ct))
            .ReturnsAsync([]);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetTimeZoneByIdQuery>(), ct))
            .ReturnsAsync(TimeZone);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        CalculateActiveCartShipmentQuery query = new(buyerId, address);
        CalculateActiveCartShipmentHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByBuyerIdAsync(buyerId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        CalculateActiveCartShipmentQuery query = new(buyerId, address);
        CalculateActiveCartShipmentHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<CalculateShipmentQuery>(),
        ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetTimeZoneByIdQuery>(),
        ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, false, ct))
            .ReturnsAsync(null as ActiveCart);

        CalculateActiveCartShipmentQuery query = new(buyerId, address);
        CalculateActiveCartShipmentHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
