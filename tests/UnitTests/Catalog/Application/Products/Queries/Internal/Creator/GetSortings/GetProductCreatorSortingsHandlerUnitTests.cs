using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

using static ProductsData;

public class GetProductCreatorSortingsHandlerUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Arrange
        GetProductCreatorSortingsQuery query = new();
        GetProductCreatorSortingsHandler handler = new();

        // Act
        string[] sortings = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(sortings, Enum.GetNames<ProductCreatorSortingType>());
    }
}
