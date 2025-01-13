using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetById;
using CustomCADs.Carts.Domain.PurchasedCarts.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.GetById;

using static PurchasedCartsData;

public class GetPurchasedCartByIdUnitTests : PurchasedCartsBaseUnitTests
{
    private readonly Mock<IPurchasedCartReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly PurchasedCart cart = CreateCartWithId();
    private static readonly PurchasedCartId id = ValidId1;
    private static readonly AccountId buyerId = ValidBuyerId1;

    public GetPurchasedCartByIdUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(cart);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetTimeZoneByIdQuery>(), ct))
            .ReturnsAsync("Europe/Sofia");
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetPurchasedCartByIdQuery query = new(id, buyerId);
        GetPurchasedCartByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetPurchasedCartByIdQuery query = new(id, buyerId);
        GetPurchasedCartByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetTimeZoneByIdQuery>(),
        ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetPurchasedCartByIdQuery query = new(id, buyerId);
        GetPurchasedCartByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        var cart = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(this.cart.Id, cart.Id);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(null as PurchasedCart);

        GetPurchasedCartByIdQuery query = new(id, buyerId);
        GetPurchasedCartByIdHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<PurchasedCartNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
