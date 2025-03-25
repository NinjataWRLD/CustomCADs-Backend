using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.UserRole;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUserRole.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUserRole;

public class GetUserRoleByIdHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly Mock<IAccountReads> reads = new();

    [Theory]
    [ClassData(typeof(GetUserRoleByIdValidData))]
    public async Task Handle_ShouldQueryDatabase(AccountId id)
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(CreateAccount());

        GetUserRoleByIdQuery query = new(id);
        GetUserRoleByIdHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetUserRoleByIdValidData))]
    public async Task Handle_ShouldReturnProperly_WhenAccountFound(AccountId id)
    {
        // Arrange
        const string role = RolesData.ValidName1;
        reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(CreateAccount(role: role));

        GetUserRoleByIdQuery query = new(id);
        GetUserRoleByIdHandler handler = new(reads.Object);

        // Act
        string actualRole = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(role, actualRole);
    }

    [Theory]
    [ClassData(typeof(GetUserRoleByIdValidData))]
    public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists(AccountId id)
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(null as Account);

        GetUserRoleByIdQuery query = new(id);
        GetUserRoleByIdHandler handler = new(reads.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Account>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
