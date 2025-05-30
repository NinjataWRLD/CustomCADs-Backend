using CustomCADs.Categories.Application.Categories.Events.Domain;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Events.Domain.Deleted;

using static CachingKeys;
using static CategoriesData;

public class CategoryDeletedHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly CategoryDeletedEventHandler handler;
	private readonly Mock<ICacheService> cache = new();

	public CategoryDeletedHandlerUnitTests()
	{
		handler = new(cache.Object);
	}

	[Fact]
	public async Task Handle_ShouldUpdateCache()
	{
		// Arrange
		CategoryDeletedDomainEvent de = new(ValidId);

		// Act
		await handler.Handle(de);

		// Assert
		cache.Verify(v => v.RemoveAsync<IEnumerable<Category>>(CategoryKey), Times.Once());
		cache.Verify(v => v.RemoveAsync<Category>($"{CategoryKey}/{de.Id}"), Times.Once());
	}
}
