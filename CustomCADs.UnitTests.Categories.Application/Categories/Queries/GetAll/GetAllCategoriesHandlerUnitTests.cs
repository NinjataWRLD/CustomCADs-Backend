using CustomCADs.Categories.Application.Categories.Queries.GetAll;
using CustomCADs.Categories.Application.Common.Caching;
using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.GetAll;

using static CachingKeys;
using static CategoriesData;

public class GetAllCategoriesHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly Mock<ICategoryReads> reads = new();
    private readonly Mock<ICacheService> cache = new();
    private readonly Category[] categories = [
        Category.CreateWithId(ValidId1, ValidName1, ValidDescription1),
        Category.CreateWithId(ValidId2, ValidName2, ValidDescription2),
        Category.CreateWithId(ValidId3, ValidName3, ValidDescription3)
    ];

    public GetAllCategoriesHandlerUnitTests()
    {
        cache.Setup(v => v.GetAsync<IEnumerable<Category>>(CategoryKey)).ReturnsAsync(categories);
        reads.Setup(v => v.AllAsync(false, ct)).ReturnsAsync(categories);
    }

    [Fact]
    public async Task Handle_ShouldCallCache_OnCacheHit()
    {
        // Assert
        GetAllCategoriesQuery query = new();
        GetAllCategoriesHandler handler = new(reads.Object, cache.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        cache.Verify(v => v.GetAsync<IEnumerable<Category>>(CategoryKey), Times.Once());
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase_OnCacheMiss()
    {
        // Assert
        cache.Setup(v => v.GetAsync<IEnumerable<Category>>(CategoryKey)).ReturnsAsync(null as Category[]);

        GetAllCategoriesQuery query = new();
        GetAllCategoriesHandler handler = new(reads.Object, cache.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(v => v.AllAsync(false, ct), Times.Once());
    }

    [Fact]
    public async Task Handle_ShouldReturnResult_OnCacheHit()
    {
        // Assert
        GetAllCategoriesQuery query = new();
        GetAllCategoriesHandler handler = new(reads.Object, cache.Object);

        // Act
        IEnumerable<CategoryReadDto> categories = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.Equal(categories.Select(r => r.Id), this.categories.Select(r => r.Id));
            Assert.Equal(categories.Select(r => r.Name), this.categories.Select(r => r.Name));
            Assert.Equal(categories.Select(r => r.Description), this.categories.Select(r => r.Description));
        });
    }

    [Fact]
    public async Task Handle_ShouldReturnResult_OnCacheMiss()
    {
        // Assert
        cache.Setup(v => v.GetAsync<IEnumerable<Category>>(CategoryKey)).ReturnsAsync(null as Category[]);
        GetAllCategoriesQuery query = new();
        GetAllCategoriesHandler handler = new(reads.Object, cache.Object);

        // Act
        IEnumerable<CategoryReadDto> categories = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.Equal(categories.Select(r => r.Id), this.categories.Select(r => r.Id));
            Assert.Equal(categories.Select(r => r.Name), this.categories.Select(r => r.Name));
            Assert.Equal(categories.Select(r => r.Description), this.categories.Select(r => r.Description));
        });
    }
}
