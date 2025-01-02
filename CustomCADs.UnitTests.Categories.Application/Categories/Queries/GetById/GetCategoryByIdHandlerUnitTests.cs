using CustomCADs.Categories.Application.Categories.Queries.GetById;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Shared.Application.Cache;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.UnitTests.Categories.Application.Categories.Queries.GetById.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.GetById;

using static CachingKeys;
using static CategoriesData;

public class GetCategoryByIdHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly Mock<ICategoryReads> reads = new();
    private readonly Mock<ICacheService> cache = new();

    public GetCategoryByIdHandlerUnitTests()
    {
        reads.Setup(v => v.SingleByIdAsync(ValidId1, false, ct)).ReturnsAsync(CreateCategory(ValidId1, ValidName1, ValidDescription1));
        reads.Setup(v => v.SingleByIdAsync(ValidId2, false, ct)).ReturnsAsync(CreateCategory(ValidId2, ValidName2, ValidDescription2));
        reads.Setup(v => v.SingleByIdAsync(ValidId3, false, ct)).ReturnsAsync(CreateCategory(ValidId3, ValidName3, ValidDescription3));

        cache.Setup(v => v.GetAsync<Category>($"{CategoryKey}/{ValidId1}")).ReturnsAsync(CreateCategory(ValidId1, ValidName1, ValidDescription1));
        cache.Setup(v => v.GetAsync<Category>($"{CategoryKey}/{ValidId2}")).ReturnsAsync(CreateCategory(ValidId2, ValidName2, ValidDescription2));
        cache.Setup(v => v.GetAsync<Category>($"{CategoryKey}/{ValidId3}")).ReturnsAsync(CreateCategory(ValidId3, ValidName3, ValidDescription3));
    }

    [Theory]
    [ClassData(typeof(GetCategoryByNameValidData))]
    public async Task Handle_ShouldCallCache_WhenCacheHit(CategoryId id)
    {
        // Arrange
        GetCategoryByIdQuery query = new(id);
        GetCategoryByIdHandler handler = new(reads.Object, cache.Object);

        // Act
        await handler.Handle(query, ct);
        
        // Assert
        cache.Verify(v => v.GetAsync<Category>($"{CategoryKey}/{id}"), Times.Once());
    }

    [Theory]
    [ClassData(typeof(GetCategoryByNameValidData))]
    public async Task Handle_ShouldQueryDatabase_WhenCacheMiss(CategoryId id)
    {
        // Arrange
        cache.Setup(v => v.GetAsync<Category>($"{CategoryKey}/{id}")).ReturnsAsync(null as Category);

        GetCategoryByIdQuery query = new(id);
        GetCategoryByIdHandler handler = new(reads.Object, cache.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(v => v.SingleByIdAsync(id, false, ct), Times.Once());
    }

    [Theory]
    [ClassData(typeof(GetCategoryByNameValidData))]
    public async Task Handle_ShouldUpdateCache_WhenDatabaseHit(CategoryId id)
    {
        // Arrange
        cache.Setup(v => v.GetAsync<Category>($"{CategoryKey}/{id}")).ReturnsAsync(null as Category);

        GetCategoryByIdQuery query = new(id);
        GetCategoryByIdHandler handler = new(reads.Object, cache.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        Category category = CreateCategory(id);
        cache.Verify(v => v.SetAsync(
            $"{CategoryKey}/{id}",
            It.Is<Category>(c => c.Id == category.Id)
        ), Times.Once());
    }

    [Theory]
    [ClassData(typeof(GetCategoryByNameValidData))]
    public async Task Handle_ShouldThrowException_WhenDatabaseMiss(CategoryId id)
    {
        // Arrange
        cache.Setup(v => v.GetAsync<Category>($"{CategoryKey}/{id}")).ReturnsAsync(null as Category);
        reads.Setup(v => v.SingleByIdAsync(id, false, ct)).ReturnsAsync(null as Category);

        GetCategoryByIdQuery query = new(id);
        GetCategoryByIdHandler handler = new(reads.Object, cache.Object);

        // Assert
        await Assert.ThrowsAsync<CategoryNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
