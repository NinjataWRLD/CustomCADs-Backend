using CustomCADs.Categories.Application.Categories.Events.Domain;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Events.Domain.Edited;

using Data;
using static CachingKeys;
using static CategoriesData;

public class CategoryEditedHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly CategoryEditedEventHandler handler;
    private readonly Mock<ICacheService> cache = new();

    public CategoryEditedHandlerUnitTests()
    {
        handler = new(cache.Object);
    }

    [Theory]
    [ClassData(typeof(CategoryEditedValidData))]
    public async Task Handle_ShouldUpdateCache(string name, string description)
    {
        // Arrange
        Category category = CreateCategory(ValidId, name, description);
        CategoryEditedDomainEvent de = new(category.Id, category);

        // Act
        await handler.Handle(de);

        // Assert
        cache.Verify(v => v.RemoveAsync<IEnumerable<Category>>(CategoryKey), Times.Once());
        cache.Verify(v => v.SetAsync($"{CategoryKey}/{category.Id}", category), Times.Once());
    }
}
