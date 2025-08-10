using CustomCADs.Catalog.Application.Categories.Queries.Internal.GetById;
using CustomCADs.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Queries.Internal.GetById;

using static CategoriesData;

public class GetCategoryByIdHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly GetCategoryByIdHandler handler;
	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	public GetCategoryByIdHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(v => v.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Category>>>()))
			.ReturnsAsync(CreateCategory(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetCategoryByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(
			v => v.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Category>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCategoryByIdQuery query = new(ValidId);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(ValidId, result.Id);
	}
}
