using CustomCADs.Orders.Application.CompletedOrders.Queries.GetAll;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Queries.GetAll;

using static CompletedOrdersData;

public class GetAllCompletedOrdersHandlerUnitTests : CompletedOrdersBaseUnitTests
{
    private readonly Mock<ICompletedOrderReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private readonly Pagination pagination;
    private readonly CompletedOrderQuery query;
    private readonly CompletedOrder[] orders = [
        CreateOrderWithId(id: ValidId1, buyerId: ValidBuyerId1),
        CreateOrderWithId(id: ValidId2, buyerId: ValidBuyerId2),
    ];

    public GetAllCompletedOrdersHandlerUnitTests()
    {
        pagination = new(1, orders.Length);
        query = new(pagination);

        reads.Setup(x => x.AllAsync(query, false, ct))
            .ReturnsAsync(new Result<CompletedOrder>(orders.Length, orders));

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUsernamesByIdsQuery>(), ct))
            .ReturnsAsync(new Dictionary<AccountId, string>()
            {
                [ValidBuyerId1] = "NinjataWRLD",
                [ValidBuyerId2] = "For7a7a",
                [ValidDesignerId1] = "Oracl3000",
                [ValidDesignerId2] = "PDMatsaliev",
            });

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetTimeZonesByIdsQuery>(), ct))
            .ReturnsAsync(new Dictionary<AccountId, string>()
            {
                [ValidBuyerId1] = "Europe/Sofia",
                [ValidBuyerId2] = "Europe/Bucharest",
            });
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetAllCompletedOrdersQuery query = new(pagination);
        GetAllCompletedOrdersHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.AllAsync(this.query, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetAllCompletedOrdersQuery query = new(pagination);
        GetAllCompletedOrdersHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetTimeZonesByIdsQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetAllCompletedOrdersQuery query = new(pagination);
        GetAllCompletedOrdersHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(result.Count, orders.Length);
    }
}
