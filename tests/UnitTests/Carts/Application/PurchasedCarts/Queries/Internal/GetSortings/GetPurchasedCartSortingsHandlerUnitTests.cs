using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetSortings;
using CustomCADs.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetSortings;

public class GetPurchasedCartSortingsHandlerUnitTests : PurchasedCartsBaseUnitTests
{
    private readonly GetPurchasedCartSortingsHandler handler = new();

    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Arrange
        GetPurchasedCartSortingsQuery query = new();

        // Act
        string[] sortings = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(sortings, Enum.GetNames<PurchasedCartSortingType>());
    }
}
