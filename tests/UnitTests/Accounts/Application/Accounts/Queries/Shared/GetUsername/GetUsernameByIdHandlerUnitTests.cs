using CustomCADs.Accounts.Application.Accounts.Queries.Shared.Username;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUsername;

using static AccountsData;

public class GetUsernameByIdHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly GetUsernameByIdHandler handler;
    private readonly Mock<IAccountReads> reads = new();

    public GetUsernameByIdHandlerUnitTests()
    {
        handler = new(reads.Object);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateAccount());
        GetUsernameByIdQuery query = new(ValidId);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly_WhenAccountFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateAccount(username: ValidUsername1));
        GetUsernameByIdQuery query = new(ValidId);

        // Act
        string actualUsername = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(ValidUsername1, actualUsername);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Account);
        GetUsernameByIdQuery query = new(ValidId);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
            // Act
            async () => await handler.Handle(query, ct)
        );
    }
}
