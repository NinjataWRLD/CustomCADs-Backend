using CustomCADs.Categories.Application.Categories.Events.Domain;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.UnitTests.Categories.Application.Categories.Events.Domain.Created.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Events.Domain.Created;

using static CachingKeys;
using static CategoriesData;

public class CategoryCreatedHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly CategoryCreatedEventHandler handler;
    private readonly Mock<ICacheService> cache = new();

    public CategoryCreatedHandlerUnitTests()
    {
        handler = new(cache.Object);
    }

    [Theory]
    [ClassData(typeof(CategoryCreatedValidData))]
    public async Task Handle_ShouldUpdateCache(string name, string description)
    {
        // Arrange
        CategoryCreatedDomainEvent de = new(
            Category: CreateCategory(ValidId1, name, description)
        );

        // Act
        await handler.Handle(de);

        // Assert
        cache.Verify(v => v.RemoveAsync<IEnumerable<Category>>(CategoryKey), Times.Once());
        cache.Verify(v => v.SetAsync($"{CategoryKey}/{de.Category.Id}", de.Category), Times.Once());
    }
}
