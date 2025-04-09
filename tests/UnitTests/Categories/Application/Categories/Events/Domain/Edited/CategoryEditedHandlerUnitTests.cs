using CustomCADs.Categories.Application.Categories.Events.Domain;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.UnitTests.Categories.Application.Categories.Events.Domain.Edited.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Events.Domain.Edited;

using static CachingKeys;
using static CategoriesData;

public class CategoryEditedHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly Mock<ICacheService> cache = new();

    [Theory]
    [ClassData(typeof(CategoryEditedValidData))]
    public async Task Handle_ShouldUpdateCache(string name, string description)
    {
        // Arrange
        Category category = CreateCategory(ValidId1, name, description);
        CategoryEditedDomainEvent de = new(category.Id, category);
        CategoryEditedEventHandler handler = new(cache.Object);

        // Act
        await handler.Handle(de);

        // Assert
        cache.Verify(v => v.RemoveAsync<IEnumerable<Category>>(CategoryKey), Times.Once());
        cache.Verify(v => v.SetAsync($"{CategoryKey}/{de.Category.Id}", de.Category), Times.Once());
    }
}
