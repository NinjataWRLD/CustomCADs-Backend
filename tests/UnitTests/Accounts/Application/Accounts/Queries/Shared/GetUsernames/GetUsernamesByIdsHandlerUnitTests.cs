using CustomCADs.Accounts.Application.Accounts.Queries.Shared.Username;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUsernames.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUsernames;

using static Constants.Roles;
using static Constants.Users;

public class GetUsernamesByIdsHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly GetUsernamesByIdsHandler handler;
    private readonly Mock<IAccountReads> reads = new();
    private static readonly string[] usernames = [CustomerUsername, ContributorUsername, DesignerUsername, AdminUsername];

    public GetUsernamesByIdsHandlerUnitTests()
    {
        handler = new(reads.Object);
    }

    [Theory]
    [ClassData(typeof(GetUsernamesByIdsValidData))]
    public async Task Handle_ShouldQueryDatabase(params AccountId[] ids)
    {
        // Arrange
        AccountQuery accountQuery = new(Pagination: new(1, ids.Length), Ids: ids);
        reads.Setup(x => x.AllAsync(accountQuery, false, ct)).ReturnsAsync(new Result<Account>(
                Count: ids.Length,
                Items: [
                    CreateAccountWithId(AccountId.New(), Customer),
                CreateAccountWithId(AccountId.New(), Contributor),
                CreateAccountWithId(AccountId.New(), Designer),
                CreateAccountWithId(AccountId.New(), Admin),
                ]
            ));
        GetUsernamesByIdsQuery query = new(ids);

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
                CreateAccountWithId(AccountId.New(), Customer, username: usernames[0]),
                CreateAccountWithId(AccountId.New(), Contributor, username: usernames[1]),
                CreateAccountWithId(AccountId.New(), Designer, username: usernames[2]),
                CreateAccountWithId(AccountId.New(), Admin, username: usernames[3]),
            ]
        ));
        GetUsernamesByIdsQuery query = new(ids);

        // Act
        Dictionary<AccountId, string> result = await handler.Handle(query, ct);
        string[] actualUsernames = [.. result.Select(kvp => kvp.Value)];

        // Assert
        Assert.Equal(actualUsernames, usernames);
    }
}
