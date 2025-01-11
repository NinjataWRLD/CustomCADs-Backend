using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetAll;
using CustomCADs.Carts.Domain.PurchasedCarts.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.GetAll;

using static PurchasedCartsData;

public class GetAllPurchasedCartsUnitTests : PurchasedCartsBaseUnitTests
{
    private readonly Mock<IPurchasedCartReads> reads= new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly PurchasedCart[] carts = [
        CreateCartWithId(id: ValidId1),
        CreateCartWithId(id: ValidId2),
    ];
    private readonly PurchasedCartQuery query;

    public GetAllPurchasedCartsUnitTests()
    {
        query = new(
            Pagination: new(1, carts.Length)
        );

        reads.Setup(x => x.AllAsync(query, false, ct))
            .ReturnsAsync(new Result<PurchasedCart>(
                carts.Length, 
                carts
            ));

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetTimeZonesByIdsQuery>(), ct))
            .ReturnsAsync(new Dictionary<AccountId, string>() 
            { 
                [ValidBuyerId1] = "Europe/Sofia" 
            });
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetAllPurchasedCartsQuery query = new(this.query.Pagination);
        GetAllPurchasedCartsHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.AllAsync(this.query, false, ct), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetAllPurchasedCartsQuery query = new(this.query.Pagination);
        GetAllPurchasedCartsHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetTimeZonesByIdsQuery>(), 
        ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetAllPurchasedCartsQuery query = new(this.query.Pagination);
        GetAllPurchasedCartsHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        int expectedCount = carts.Length, actualCount = result.Count;
        PurchasedCartId[] expectedIds = [.. carts.Select(x => x.Id)],
            actualIds = [.. result.Items.Select(x => x.Id)];

        Assert.Multiple(
            () => Assert.Equal(expectedCount, actualCount),
            () => Assert.Equal(expectedIds, actualIds)
        );
    }
}
