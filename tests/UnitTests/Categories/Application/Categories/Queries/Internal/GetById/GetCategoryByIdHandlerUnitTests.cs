using CustomCADs.Categories.Application.Categories.Queries.Internal.GetById;
using CustomCADs.Categories.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.Internal.GetById;

using static CategoriesData;

public class GetCategoryByIdHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly GetCategoryByIdHandler handler;
	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	public GetCategoryByIdHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);
		Category category = CreateCategory(ValidId, ValidName, ValidDescription);

		cache.Setup(v => v.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Category>>>())).ReturnsAsync(category);
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
}
