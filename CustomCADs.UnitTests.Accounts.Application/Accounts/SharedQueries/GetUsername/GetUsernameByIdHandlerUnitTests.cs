using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.Username;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUsername.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUsername;

using static AccountsData;

public class GetUsernameByIdHandlerData : TheoryData<AccountId>;

public class GetUsernameByIdHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly IAccountReads reads = Substitute.For<IAccountReads>();

    [Theory]
    [ClassData(typeof(GetUsernameByIdHandlerValidData))]
    public async Task Handle_CallsDatabase(AccountId id)
    {
        // Arrange
        reads.SingleByIdAsync(id, false, ct).Returns(CreateAccount());

        GetUsernameByIdQuery query = new(id);
        GetUsernameByIdHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, false, ct);
    }

    [Theory]
    [ClassData(typeof(GetUsernameByIdHandlerValidData))]
    public async Task Handle_ShouldReturnProperly_WhenAccountExists(AccountId id)
    {
        // Arrange
        reads.SingleByIdAsync(id, false, ct).Returns(CreateAccount(username: ValidUsername1));

        GetUsernameByIdQuery query = new(id);
        GetUsernameByIdHandler handler = new(reads);

        // Act
        string actualUsername = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(ValidUsername1, actualUsername);
    }

    [Theory]
    [ClassData(typeof(GetUsernameByIdHandlerValidData))]
    public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists(AccountId id)
    {
        // Arrange
        reads.SingleByIdAsync(id, false, ct).Returns(null as Account);

        GetUsernameByIdQuery query = new(id);
        GetUsernameByIdHandler handler = new(reads);

        // Assert
        await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
