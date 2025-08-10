using CustomCADs.Catalog.Application.Categories.Dtos;
using CustomCADs.Catalog.Application.Categories.Queries.Internal.GetAll;
using CustomCADs.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Queries.Internal.GetAll;

using static CategoriesData;

public class GetAllCategoriesHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly GetAllCategoriesHandler handler;
	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	private readonly Category[] categories = [
		Category.CreateWithId(CategoryId.New(1), ValidName, ValidDescription),
		Category.CreateWithId(CategoryId.New(2), MinValidName, MinValidDescription),
		Category.CreateWithId(CategoryId.New(3), MaxValidName, MaxValidDescription)
	];

	public GetAllCategoriesHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(v => v.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Category>>>>())).ReturnsAsync(categories);
		reads.Setup(v => v.AllAsync(false, ct)).ReturnsAsync(categories);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetAllCategoriesQuery query = new();

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(v => v.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Category>>>>()), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAllCategoriesQuery query = new();

		// Act
		IEnumerable<CategoryReadDto> categories = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(categories.Select(r => r.Id), this.categories.Select(r => r.Id));
	}
}
