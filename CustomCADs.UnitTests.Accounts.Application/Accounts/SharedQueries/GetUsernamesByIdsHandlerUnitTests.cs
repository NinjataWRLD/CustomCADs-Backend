﻿using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.Username;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries;

using static Constants.Roles;
using static Constants.Users;

public class GetUsernamesByIdsHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly IAccountReads reads = Substitute.For<IAccountReads>();
    private static readonly string[] usernames = [ClientUsername, ContributorUsername, DesignerUsername, AdminUsername];

    [Theory]
    [InlineData(ClientAccountId, ContributorAccountId, DesignerAccountId, AdminAccountId)]
    public async Task Handle_CallsDatabase(params string[] ids)
    {
        // Arrange
        AccountId[] accountIds = [.. ids.Select(id => new AccountId(Guid.Parse(id)))];
        AccountQuery accountQuery = new(Pagination: new(1, accountIds.Length), Ids: accountIds);

        reads.AllAsync(accountQuery, false, ct).Returns(new Result<Account>(
            Count: accountIds.Length,
            Items: [
                CreateAccount(Client),
                CreateAccount(Contributor),
                CreateAccount(Designer),
                CreateAccount(Admin),
            ]
        ));

        GetUsernamesByIdsQuery query = new(accountIds);
        GetUsernamesByIdsHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).AllAsync(accountQuery, false, ct);
    }

    [Theory]
    [InlineData(ClientAccountId, ContributorAccountId, DesignerAccountId, AdminAccountId)]
    public async Task Handle_ShouldReturnProperly(params string[] ids)
    {
        // Arrange
        AccountId[] accountIds = [.. ids.Select(id => new AccountId(Guid.Parse(id)))];
        AccountQuery accountQuery = new(Pagination: new(1, accountIds.Length), Ids: accountIds);

        reads.AllAsync(accountQuery, false, ct).Returns(new Result<Account>(
            Count: accountIds.Length,
            Items: [
                CreateAccount(Client, username: usernames[0]),
                CreateAccount(Contributor, username: usernames[1]),
                CreateAccount(Designer, username: usernames[2]),
                CreateAccount(Admin, username: usernames[3]),
            ]
        ));

        GetUsernamesByIdsQuery query = new(accountIds);
        GetUsernamesByIdsHandler handler = new(reads);

        // Act
        string[] actualUsernames = [.. (await handler.Handle(query, ct)).Select(t => t.Username)];

        // Assert
        Assert.Equal(actualUsernames, usernames);
    }
}
