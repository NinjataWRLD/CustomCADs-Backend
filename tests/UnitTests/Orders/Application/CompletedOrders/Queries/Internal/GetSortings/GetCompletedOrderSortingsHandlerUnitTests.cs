using CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.GetSortings;
using CustomCADs.Orders.Domain.CompletedOrders.Enums;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Queries.Internal.GetSortings;

using static CompletedOrdersData;

public class GetCompletedOrderSortingsHandlerUnitTests : CompletedOrdersBaseUnitTests
{
    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Arrange
        GetCompletedOrderSortingsQuery query = new();
        GetCompletedOrderSortingsHandler handler = new();

        // Act
        string[] sortings = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(sortings, Enum.GetNames<CompletedOrderSortingType>());
    }
}
