using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetSortings;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Gallery.GetSortings;

public class GetProductSortingsHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly GetProductGallerySortingsHandler handler = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetProductGallerySortingsQuery query = new();

		// Act
		string[] sortings = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetNames<ProductGallerySortingType>());
	}
}
