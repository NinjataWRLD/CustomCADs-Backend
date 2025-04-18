using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

using static ProductsData;

public class GetProductCreatorSortingsHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly GetProductCreatorSortingsHandler handler = new();

    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Arrange
        GetProductCreatorSortingsQuery query = new();

        // Act
        string[] sortings = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(sortings, Enum.GetNames<ProductCreatorSortingType>());
    }
}
