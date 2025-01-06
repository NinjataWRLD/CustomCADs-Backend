using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.Username;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUsernames.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUsernames;

using static Constants.Roles;
using static Constants.Users;

public class GetUsernamesByIdsHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly Mock<IAccountReads> reads = new();
    private static readonly string[] usernames = [ClientUsername, ContributorUsername, DesignerUsername, AdminUsername];

    [Theory]
    [ClassData(typeof(GetUsernamesByIdsValidData))]
    public async Task Handle_ShouldQueryDatabase(params AccountId[] ids)
    {
        // Arrange
        AccountQuery accountQuery = new(Pagination: new(1, ids.Length), Ids: ids);

        reads.Setup(x => x.AllAsync(accountQuery, false, ct)).ReturnsAsync(new Result<Account>(
            Count: ids.Length,
            Items: [
                CreateAccount(Client),
                CreateAccount(Contributor),
                CreateAccount(Designer),
                CreateAccount(Admin),
            ]
        ));

        GetUsernamesByIdsQuery query = new(ids);
        GetUsernamesByIdsHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.AllAsync(accountQuery, false, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetUsernamesByIdsValidData))]
    public async Task Handle_ShouldReturnProperly(params AccountId[] ids)
    {
        // Arrange
        AccountQuery accountQuery = new(Pagination: new(1, ids.Length), Ids: ids);

        reads.Setup(x => x.AllAsync(accountQuery, false, ct)).ReturnsAsync(new Result<Account>(
            Count: ids.Length,
            Items: [
                CreateAccount(Client, username: usernames[0]),
                CreateAccount(Contributor, username: usernames[1]),
                CreateAccount(Designer, username: usernames[2]),
                CreateAccount(Admin, username: usernames[3]),
            ]
        ));

        GetUsernamesByIdsQuery query = new(ids);
        GetUsernamesByIdsHandler handler = new(reads.Object);

        // Act
        string[] actualUsernames = [.. (await handler.Handle(query, ct)).Select(t => t.Username)];

        // Assert
        Assert.Equal(actualUsernames, usernames);
    }
}
