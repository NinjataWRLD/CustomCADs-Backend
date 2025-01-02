using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.UserRole;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUserRole.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUserRole;

public class GetUserRoleByIdHandlerData : TheoryData<AccountId>;

public class GetUserRoleByIdHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly IAccountReads reads = Substitute.For<IAccountReads>();

    [Theory]
    [ClassData(typeof(GetUserRoleByIdHandlerValidData))]
    public async Task Handle_ShouldQueryDatabase(AccountId id)
    {
        // Arrange
        reads.SingleByIdAsync(id, false, ct).Returns(CreateAccount());

        GetUserRoleByIdQuery query = new(id);
        GetUserRoleByIdHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, false, ct);
    }

    [Theory]
    [ClassData(typeof(GetUserRoleByIdHandlerValidData))]
    public async Task Handle_ShouldReturnProperly_WhenAccountFound(AccountId id)
    {
        // Arrange
        const string role = RolesData.ValidName1;
        reads.SingleByIdAsync(id, false, ct).Returns(CreateAccount(role: role));

        GetUserRoleByIdQuery query = new(id);
        GetUserRoleByIdHandler handler = new(reads);

        // Act
        string actualRole = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(role, actualRole);
    }

    [Theory]
    [ClassData(typeof(GetUserRoleByIdHandlerValidData))]
    public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists(AccountId id)
    {
        // Arrange
        reads.SingleByIdAsync(id, false, ct).Returns(null as Account);

        GetUserRoleByIdQuery query = new(id);
        GetUserRoleByIdHandler handler = new(reads);

        // Assert
        await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
