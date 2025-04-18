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

    private readonly static CategoryId[] ids = [ValidId1, ValidId2, ValidId3];
    private static readonly (CategoryId Id, string Name)[] categories = [
        (ValidId1, ValidName1),
        (ValidId2, ValidName2),
        (ValidId3, ValidName3),
    ];

    public GetCategoryNamesByIdsHandlerUnitTests()
    {
        handler = new(reads.Object);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        reads.Setup(v => v.AllAsync(false, ct)).ReturnsAsync(
            [.. categories.Select(c => CreateCategory(c.Id, c.Name, ValidDescription1))]
        );
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
        reads.Setup(v => v.AllAsync(false, ct)).ReturnsAsync(
            [.. categories.Select(c => CreateCategory(c.Id, c.Name, ValidDescription1))]
        );
        GetCategoryNamesByIdsQuery query = new(ids);

        // Act
        var actualCategories = (await handler.Handle(query, ct)).Select(x => (x.Key, x.Value));

        // Assert
        Assert.Equal(actualCategories, categories);
    }
}
