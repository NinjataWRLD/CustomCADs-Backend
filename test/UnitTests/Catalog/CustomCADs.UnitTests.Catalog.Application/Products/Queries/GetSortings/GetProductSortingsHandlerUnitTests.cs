using CustomCADs.Accounts.Application.Accounts.Queries.GetSortings;
using CustomCADs.Catalog.Application.Products.Queries.GetSortings;
using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetSortings;

using static ProductsData;

public class GetProductSortingsHandlerUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Arrange
        GetProductSortingsQuery query = new();
        GetProductSortingsHandler handler = new();

        // Act
        string[] sortings = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(sortings, Enum.GetNames<ProductSortingType>());
    }
}
