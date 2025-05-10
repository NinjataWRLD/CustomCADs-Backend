using CustomCADs.Accounts.Application.Accounts.Queries.Shared.Exists;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetExists;

using static AccountsData;

public class GetAccountExistsByIdHandlerUnitTestsUnitTests : AccountsBaseUnitTests
{
    private readonly GetAccountExistsByIdHandler handler;
    private readonly Mock<IAccountReads> reads = new();

    public GetAccountExistsByIdHandlerUnitTestsUnitTests()
    {
        handler = new(reads.Object);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(true);
        GetAccountExistsByIdQuery query = new(ValidId);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.ExistsByIdAsync(ValidId, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly_WhenAccountExists()
    {
        // Arrange
        reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(true);
        GetAccountExistsByIdQuery query = new(ValidId);

        // Act
        bool exists = await handler.Handle(query, ct);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly_WhenAccountDoesNotExists()
    {
        // Arrange
        reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(false);
        GetAccountExistsByIdQuery query = new(ValidId);

        // Act
        bool exists = await handler.Handle(query, ct);

        // Assert
        Assert.False(exists);
    }
}
