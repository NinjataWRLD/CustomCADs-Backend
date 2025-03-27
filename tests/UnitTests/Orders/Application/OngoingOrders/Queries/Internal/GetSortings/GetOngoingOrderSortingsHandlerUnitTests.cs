using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetSortings;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.Internal.GetSortings;

using static OngoingOrdersData;

public class GetOngoingOrderSortingsHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Arrange
        GetOngoingOrderSortingsQuery query = new();
        GetOngoingOrderSortingsHandler handler = new();

        // Act
        string[] sortings = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(sortings, Enum.GetNames<OngoingOrderSortingType>());
    }
}
