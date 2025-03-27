using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetById;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetById;

using static PurchasedCartsData;

public class GetPurchasedCartByIdUnitTests : PurchasedCartsBaseUnitTests
{
    private readonly Mock<IPurchasedCartReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string Buyer = "PDMatsaliev20";
    private readonly PurchasedCart cart = CreateCartWithId();
    private static readonly PurchasedCartId id = ValidId1;
    private static readonly AccountId buyerId = ValidBuyerId1;

    public GetPurchasedCartByIdUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(cart);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetTimeZoneByIdQuery>(), ct))
            .ReturnsAsync("Europe/Sofia");

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUsernameByIdQuery>(), ct))
            .ReturnsAsync(Buyer);
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
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetUsernameByIdQuery>(),
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
        await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
