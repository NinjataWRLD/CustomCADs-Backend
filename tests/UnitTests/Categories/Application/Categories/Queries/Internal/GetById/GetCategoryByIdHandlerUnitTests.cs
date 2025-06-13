using CustomCADs.Categories.Application.Categories.Queries.Internal.GetById;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.Internal.GetById;

using static CachingKeys;
using static CategoriesData;

public class GetCategoryByIdHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly GetCategoryByIdHandler handler;
	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<ICacheService> cache = new();

	public GetCategoryByIdHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		reads.Setup(v => v.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateCategory(ValidId, ValidName, ValidDescription));
		cache.Setup(v => v.GetAsync<Category>($"{CategoryKey}/{ValidId}")).ReturnsAsync(CreateCategory(ValidId, ValidName, ValidDescription));
	}

	[Fact]
	public async Task Handle_ShouldCallCache_WhenCacheHit()
	{
		// Arrange
		GetCategoryByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(v => v.GetAsync<Category>($"{CategoryKey}/{ValidId}"), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase_WhenCacheMiss()
	{
		// Arrange
		cache.Setup(v => v.GetAsync<Category>($"{CategoryKey}/{ValidId}")).ReturnsAsync(null as Category);

		GetCategoryByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldUpdateCache_WhenDatabaseHit()
	{
		// Arrange
		cache.Setup(v => v.GetAsync<Category>($"{CategoryKey}/{ValidId}")).ReturnsAsync(null as Category);
		GetCategoryByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		Category category = CreateCategory(ValidId);
		cache.Verify(v => v.SetAsync(
			$"{CategoryKey}/{ValidId}",
			It.Is<Category>(c => c.Id == category.Id)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenDatabaseMiss()
	{
		// Arrange
		cache.Setup(v => v.GetAsync<Category>($"{CategoryKey}/{ValidId}")).ReturnsAsync(null as Category);
		reads.Setup(v => v.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Category);
		GetCategoryByIdQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Category>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
