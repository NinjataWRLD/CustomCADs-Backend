using CustomCADs.Categories.Application.Categories.Queries.Shared;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.Shared.GetByIds;

using static CategoriesData;

public class GetCategoryNamesByIdsHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly GetCategoryNamesByIdsHandler handler;
    private readonly Mock<ICategoryReads> reads = new();

    private readonly static CategoryId[] ids = [
        CategoryId.New(1),
        CategoryId.New(2),
        CategoryId.New(3)
    ];
    private static readonly Category[] categories = [.. ids.Select(id => CreateCategory(id, ValidName1, ValidDescription1))];

    public GetCategoryNamesByIdsHandlerUnitTests()
    {
        handler = new(reads.Object);
        reads.Setup(x => x.AllAsync(false, ct)).ReturnsAsync(categories);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetCategoryNamesByIdsQuery query = new(ids);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(v => v.AllAsync(false, ct), Times.Once());
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetCategoryNamesByIdsQuery query = new(ids);

        // Act
        var actualCategories = (await handler.Handle(query, ct)).Select(x => (x.Key, x.Value));

        // Assert
        Assert.Equal(actualCategories, [.. categories.Select(c => (c.Id, c.Name))]);
    }
}
