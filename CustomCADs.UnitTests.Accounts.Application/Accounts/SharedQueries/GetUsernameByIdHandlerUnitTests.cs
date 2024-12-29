using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.Username;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries;

using static Constants.Roles;
using static Constants.Users;

public class GetUsernameByIdHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly IAccountReads reads = Substitute.For<IAccountReads>();
    private static AccountId Id => new(Guid.Parse("f8f8f8f8-f8f8-f8f8-f8f8-f8f8f8f8f8f8"));

    [Theory]
    [InlineData(Client)]
    [InlineData(Contributor)]
    [InlineData(Designer)]
    [InlineData(Admin)]
    public async Task Handle_CallsDatabase(string role)
    {
        // Arrange
        reads.SingleByIdAsync(Id, false, ct).Returns(CreateAccount(role));

        GetUsernameByIdQuery query = new(Id);
        GetUsernameByIdHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(Id, false, ct);
    }

    [Theory]
    [InlineData(Client, ClientUsername)]
    [InlineData(Contributor, ContributorUsername)]
    [InlineData(Designer, DesignerUsername)]
    [InlineData(Admin, AdminUsername)]
    public async Task Handle_ShouldReturnProperly_WhenAccountExists(string role, string username)
    {
        // Arrange
        reads.SingleByIdAsync(Id, false, ct).Returns(CreateAccount(role, username: username));

        GetUsernameByIdQuery query = new(Id);
        GetUsernameByIdHandler handler = new(reads);

        // Act
        string actualUsername = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(actualUsername, username);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists()
    {
        // Arrange
        reads.SingleByIdAsync(Id, false, ct).Returns(null as Account);

        GetUsernameByIdQuery query = new(Id);
        GetUsernameByIdHandler handler = new(reads);

        // Assert
        await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
