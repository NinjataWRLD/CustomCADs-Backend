using CustomCADs.Accounts.Application.Accounts.Queries.Shared.UserRole;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUserRole;

using static AccountsData;

public class GetUserRoleByIdHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly GetUserRoleByIdHandler handler;
    private readonly Mock<IAccountReads> reads = new();

    public GetUserRoleByIdHandlerUnitTests()
    {
        handler = new(reads.Object);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateAccount());
        GetUserRoleByIdQuery query = new(ValidId);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly_WhenAccountFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateAccount(role: RolesData.ValidName1));
        GetUserRoleByIdQuery query = new(ValidId);

        // Act
        string actualRole = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(RolesData.ValidName1, actualRole);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Account);
        GetUserRoleByIdQuery query = new(ValidId);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Account>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
