using CustomCADs.Categories.Application.Categories.Queries.Shared;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.Shared.GetById;

using static CategoriesData;

public class GetCategoryByIdHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly GetCategoryNameByIdHandler handler;
    private readonly Mock<ICategoryReads> reads = new();

    public GetCategoryByIdHandlerUnitTests()
    {
        handler = new(reads.Object);

        reads.Setup(v => v.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(CreateCategory());
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetCategoryNameByIdQuery query = new(ValidId);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(v => v.SingleByIdAsync(ValidId, false, ct), Times.Once());
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDatabaseMiss()
    {
        // Arrange
        reads.Setup(v => v.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Category);
        GetCategoryNameByIdQuery query = new(ValidId);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Category>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });

    }
}
