using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetSortings;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Designer.GetSortings;

using static ProductsData;

public class GetProductDesignerSortingsHandlerUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Arrange
        GetProductDesignerSortingsQuery query = new();
        GetProductDesignerSortingsHandler handler = new();

        // Act
        string[] sortings = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(sortings, Enum.GetNames<ProductDesignerSortingType>());
    }
}
