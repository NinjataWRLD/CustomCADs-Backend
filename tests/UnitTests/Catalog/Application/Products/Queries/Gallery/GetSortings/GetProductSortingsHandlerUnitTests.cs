using CustomCADs.Catalog.Application.Common.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Gallery.GetSortings;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Gallery.GetSortings;

using static ProductsData;

public class GetProductSortingsHandlerUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Arrange
        GetProductGallerySortingsQuery query = new();
        GetProductGallerySortingsHandler handler = new();

        // Act
        string[] sortings = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(sortings, Enum.GetNames<ProductGallerySortingType>());
    }
}
