using CustomCADs.Files.Application.Cads.SharedQueryHandlers;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedQueries.GetExists;

public class GetCadExistsByIdHandlerUnitTests : CadsBaseUnitTests
{
    private readonly Mock<ICadReads> reads = new();
    private static readonly CadId id = CadId.New();

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(true);

        GetCadExistsByIdQuery query = new(id);
        GetCadExistsByIdHandler handler = new(reads.Object);

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

        GetCadExistsByIdQuery query = new(id);
        GetCadExistsByIdHandler handler = new(reads.Object);

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

        GetCadExistsByIdQuery query = new(id);
        GetCadExistsByIdHandler handler = new(reads.Object);

        // Act
        bool exists = await handler.Handle(query, ct);

        // Assert
        Assert.False(exists);
    }
}
