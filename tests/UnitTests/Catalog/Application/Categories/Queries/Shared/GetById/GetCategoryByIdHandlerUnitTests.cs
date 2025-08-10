using CustomCADs.Catalog.Application.Categories.Queries.Shared;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Queries.Shared.GetById;

using static CategoriesData;

public class GetCategoryByIdHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly GetCategoryNameByIdHandler handler;
	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	public GetCategoryByIdHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(v => v.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Category>>>()))
			.ReturnsAsync(CreateCategory(name: ValidName));
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetCategoryNameByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(v => v.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Category>>>()), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCategoryNameByIdQuery query = new(ValidId);

		// Act
		string result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(ValidName, result);
	}
}
