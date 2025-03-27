using CustomCADs.Categories.Application.Categories.EventHandlers.Domain;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.UnitTests.Categories.Application.Categories.EventHandlers.Domain.Created.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.EventHandlers.Domain.Created;

using static CachingKeys;
using static CategoriesData;

public class CategoryCreatedHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly Mock<ICacheService> cache = new();

    [Theory]
    [ClassData(typeof(CategoryCreatedValidData))]
    public async Task Handle_ShouldUpdateCache(string name, string description)
    {
        // Arrange
        Category category = CreateCategory(ValidId1, name, description);
        CategoryCreatedDomainEvent de = new(category);
        CategoryCreatedEventHandler handler = new(cache.Object);

        // Act
        await handler.Handle(de);

        // Assert
        cache.Verify(v => v.RemoveAsync<IEnumerable<Category>>(CategoryKey), Times.Once());
        cache.Verify(v => v.SetAsync($"{CategoryKey}/{de.Category.Id}", de.Category), Times.Once());
    }
}
