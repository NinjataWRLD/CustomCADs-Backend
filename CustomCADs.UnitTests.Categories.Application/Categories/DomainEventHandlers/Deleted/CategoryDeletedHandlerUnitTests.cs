using CustomCADs.Categories.Application.Categories.DomainEventHandlers;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Domain.Categories.DomainEvents;
using CustomCADs.Shared.Application.Cache;
using CustomCADs.UnitTests.Categories.Application.Categories.DomainEventHandlers.Deleted.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.DomainEventHandlers.Deleted;

using static CachingKeys;
using static CategoriesData;

public class CategoryDeletedHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly Mock<ICacheService> cache = new();

    [Theory]
    [ClassData(typeof(CategoryDeletedValidData))]
    public async Task Handle_ShouldUpdateCache(string name, string description)
    {
        // Arrange
        Category category = CreateCategory(ValidId1, name, description);
        CategoryDeletedDomainEvent de = new(category.Id);
        CategoryDeletedEventHandler handler = new(cache.Object);

        // Act
        await handler.Handle(de);

        // Assert
        cache.Verify(v => v.RemoveAsync<IEnumerable<Category>>(CategoryKey), Times.Once());
        cache.Verify(v => v.RemoveAsync<Category>($"{CategoryKey}/{de.Id}"), Times.Once());
    }
}
