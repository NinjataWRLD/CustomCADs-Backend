﻿using CustomCADs.Categories.Application.Categories.Events.Domain;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.UnitTests.Categories.Application.Categories.Events.Domain.Deleted.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Events.Domain.Deleted;

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
