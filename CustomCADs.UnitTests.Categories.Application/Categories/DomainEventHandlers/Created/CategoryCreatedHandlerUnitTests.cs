using CustomCADs.Categories.Application.Categories.DomainEventHandlers;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Application.Common.Caching.Categories;
using CustomCADs.Categories.Domain.Categories.DomainEvents;
using CustomCADs.Shared.Application.Cache;
using CustomCADs.UnitTests.Categories.Application.Categories.DomainEventHandlers.Created.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.DomainEventHandlers.Created;

using static CachingKeys;
using static CategoriesData;

public class CategoryCreatedHandlerData : TheoryData<string, string>;

public class CategoryCreatedHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly Mock<ICacheService> cache = new();

    [Theory]
    [ClassData(typeof(CategoryCreatedHandlerValidData))]
    public async Task Handle_ShouldUpdateCache(string name, string description)
    {
        // Arrange
        Category category = CreateCategory(ValidId1, name, description);
        CategoryCreatedDomainEvent de = new(category);
        CategoryCreatedEventHandler handler = new(cache.Object);

        // Act
        await handler.Handle(de);

        // Assert
        cache.Verify( v => v.RemoveAsync<IEnumerable<Category>>(CategoryKey), Times.Once());
        cache.Verify( v => v.SetAsync($"{CategoryKey}/{de.Category.Id}", de.Category), Times.Once());
    }
}
