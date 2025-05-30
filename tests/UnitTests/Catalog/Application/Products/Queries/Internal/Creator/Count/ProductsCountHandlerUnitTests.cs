using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.Count;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.Count;

using static ProductsData;

public class ProductsCountHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly ProductsCountHandler handler;
	private readonly Mock<IProductReads> reads = new();

	private readonly Dictionary<ProductStatus, int> dict = new()
	{
		[ProductStatus.Unchecked] = 3,
		[ProductStatus.Validated] = 2,
		[ProductStatus.Reported] = 1,
		[ProductStatus.Removed] = 0,
	};

	public ProductsCountHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountByStatusAsync(ValidCreatorId, ct))
			.ReturnsAsync(dict);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ProductsCountQuery query = new(ValidCreatorId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.CountByStatusAsync(ValidCreatorId, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Arrange
		ProductsCountQuery query = new(ValidCreatorId);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(dict[ProductStatus.Unchecked], result.Unchecked),
			() => Assert.Equal(dict[ProductStatus.Validated], result.Validated),
			() => Assert.Equal(dict[ProductStatus.Reported], result.Reported),
			() => Assert.Equal(dict[ProductStatus.Removed], result.Banned)
		);
	}
}
