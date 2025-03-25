using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.Username;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUsername.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUsername;

using static AccountsData;

public class GetUsernameByIdHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly Mock<IAccountReads> reads = new();

    [Theory]
    [ClassData(typeof(GetUsernameByIdValidData))]
    public async Task Handle_ShouldQueryDatabase(AccountId id)
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(CreateAccount());

        GetUsernameByIdQuery query = new(id);
        GetUsernameByIdHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetUsernameByIdValidData))]
    public async Task Handle_ShouldReturnProperly_WhenAccountFound(AccountId id)
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(CreateAccount(username: ValidUsername1));

        GetUsernameByIdQuery query = new(id);
        GetUsernameByIdHandler handler = new(reads.Object);

        // Act
        string actualUsername = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(ValidUsername1, actualUsername);
    }

    [Theory]
    [ClassData(typeof(GetUsernameByIdValidData))]
    public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists(AccountId id)
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(null as Account);

        GetUsernameByIdQuery query = new(id);
        GetUsernameByIdHandler handler = new(reads.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Account>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
