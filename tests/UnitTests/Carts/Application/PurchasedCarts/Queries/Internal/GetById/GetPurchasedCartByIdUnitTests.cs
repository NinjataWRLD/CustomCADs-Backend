using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetById;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetById;

using static PurchasedCartsData;

public class GetPurchasedCartByIdUnitTests : PurchasedCartsBaseUnitTests
{
    private readonly GetPurchasedCartByIdHandler handler;
    private readonly Mock<IPurchasedCartReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string Buyer = "PDMatsaliev20";
    private readonly PurchasedCart cart = CreateCartWithId();

    public GetPurchasedCartByIdUnitTests()
    {
        handler = new(reads.Object, sender.Object);

        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(cart);

        sender.Setup(x => x.SendQueryAsync(
            It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId),
            ct
        )).ReturnsAsync(Buyer);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetPurchasedCartByIdQuery query = new(ValidId, ValidBuyerId);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetPurchasedCartByIdQuery query = new(ValidId, ValidBuyerId);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId),
        ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetPurchasedCartByIdQuery query = new(ValidId, ValidBuyerId);

        // Act
        var cart = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(this.cart.Id, cart.Id);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(null as PurchasedCart);
        GetPurchasedCartByIdQuery query = new(ValidId, ValidBuyerId);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
