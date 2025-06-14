using CustomCADs.Categories.Application.Categories.Queries.Internal.GetAll;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.Internal.GetAll;

using static CachingKeys;
using static CategoriesData;

public class GetAllCategoriesHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly GetAllCategoriesHandler handler;
	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<ICacheService> cache = new();

	private readonly Category[] categories = [
		Category.CreateWithId(CategoryId.New(1), ValidName, ValidDescription),
		Category.CreateWithId(CategoryId.New(2), MinValidName, MinValidDescription),
		Category.CreateWithId(CategoryId.New(3), MaxValidName, MaxValidDescription)
	];

	public GetAllCategoriesHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(v => v.GetAsync<IEnumerable<Category>>(CategoryKey)).ReturnsAsync(categories);
		reads.Setup(v => v.AllAsync(false, ct)).ReturnsAsync(categories);
	}

	[Fact]
	public async Task Handle_ShouldCallCache_OnCacheHit()
	{
		// Arrange
		GetAllCategoriesQuery query = new();

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(v => v.GetAsync<IEnumerable<Category>>(CategoryKey), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase_OnCacheMiss()
	{
		// Arrange
		cache.Setup(v => v.GetAsync<IEnumerable<Category>>(CategoryKey)).ReturnsAsync(null as Category[]);
		GetAllCategoriesQuery query = new();

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(v => v.AllAsync(false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult_OnCacheHit()
	{
		// Arrange
		GetAllCategoriesQuery query = new();

		// Act
		IEnumerable<CategoryReadDto> categories = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(() =>
		{
			Assert.Equal(categories.Select(r => r.Id), this.categories.Select(r => r.Id));
			Assert.Equal(categories.Select(r => r.Name), this.categories.Select(r => r.Name));
			Assert.Equal(categories.Select(r => r.Description), this.categories.Select(r => r.Description));
		});
	}

	[Fact]
	public async Task Handle_ShouldReturnResult_OnCacheMiss()
	{
		// Arrange
		cache.Setup(v => v.GetAsync<IEnumerable<Category>>(CategoryKey)).ReturnsAsync(null as Category[]);
		GetAllCategoriesQuery query = new();

		// Act
		IEnumerable<CategoryReadDto> categories = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(() =>
		{
			Assert.Equal(categories.Select(r => r.Id), this.categories.Select(r => r.Id));
			Assert.Equal(categories.Select(r => r.Name), this.categories.Select(r => r.Name));
			Assert.Equal(categories.Select(r => r.Description), this.categories.Select(r => r.Description));
		});
	}
}
