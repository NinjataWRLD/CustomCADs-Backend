using CustomCADs.Carts.Application.ActiveCarts.Queries.GetByBuyerId;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.GetByBuyerId;

using static ActiveCartsData;

public class GetActiveCartHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private const string Buyer = "For7a7a";
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly ActiveCart cart = CreateCart(buyerId);

    public GetActiveCartHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, false, ct))
            .ReturnsAsync(cart);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUsernameByIdQuery>(), ct))
            .ReturnsAsync(Buyer);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetActiveCartQuery query = new(buyerId);
        GetActiveCartHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByBuyerIdAsync(buyerId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetActiveCartQuery query = new(buyerId);
        GetActiveCartHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetUsernameByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetActiveCartQuery query = new(buyerId);
        GetActiveCartHandler handler = new(reads.Object, sender.Object);

        // Act
        var actual = await handler.Handle(query, ct);

        // Assert
        var expected = cart;
        Assert.Multiple(
            () => Assert.Equal(expected.Id, actual.Id),
            () => Assert.Equal(expected.Items.Count, actual.Items.Count)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, false, ct))
            .ReturnsAsync(null as ActiveCart);

        GetActiveCartQuery query = new(buyerId);
        GetActiveCartHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
