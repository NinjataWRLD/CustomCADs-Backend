using CustomCADs.Categories.Application.Categories.Queries.Shared;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.Shared.GetExists;

using static CategoriesData;

public class GetCategoryExistsByIdHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly GetCategoryExistsByIdHandler handler;
    private readonly Mock<ICategoryReads> reads = new();

    private static readonly CategoryId id = ValidId1;

    public GetCategoryExistsByIdHandlerUnitTests()
    {
        handler = new(reads.Object);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(true);
        GetCategoryExistsByIdQuery query = new(id);

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

        // Act
        bool exists = await handler.Handle(query, ct);

        // Assert
        Assert.False(exists);
    }
}
