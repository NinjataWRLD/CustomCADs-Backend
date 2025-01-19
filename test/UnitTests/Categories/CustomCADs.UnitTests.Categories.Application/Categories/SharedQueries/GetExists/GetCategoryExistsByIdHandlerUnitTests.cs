using CustomCADs.Categories.Application.Categories.SharedQueryHandlers;
using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Categories.Application.Categories.SharedQueries.GetExists;

using static CategoriesData;

public class GetCategoryExistsByIdHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly Mock<ICategoryReads> reads = new();
    private static readonly CategoryId id = ValidId1;

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(true);

        GetCategoryExistsByIdQuery query = new(id);
        GetCategoryExistsByIdHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.ExistsByIdAsync(id, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly_WhenProductExists()
    {
        // Arrange
        reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(true);

        GetCategoryExistsByIdQuery query = new(id);
        GetCategoryExistsByIdHandler handler = new(reads.Object);

        // Act
        bool exists = await handler.Handle(query, ct);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly_WhenProductDoesNotExists()
    {
        // Arrange
        reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(false);

        GetCategoryExistsByIdQuery query = new(id);
        GetCategoryExistsByIdHandler handler = new(reads.Object);

        // Act
        bool exists = await handler.Handle(query, ct);

        // Assert
        Assert.False(exists);
    }
}
