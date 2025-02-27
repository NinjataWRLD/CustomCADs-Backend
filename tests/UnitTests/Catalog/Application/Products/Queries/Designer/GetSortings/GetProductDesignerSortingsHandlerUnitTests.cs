using CustomCADs.Catalog.Application.Common.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Designer.GetSortings;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Designer.GetSortings;

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
