using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.TimeZone;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries;

using static Constants.Roles;
using static Constants.Users;

public class GetTimeZonesByIdsHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly IAccountReads reads = Substitute.For<IAccountReads>();
    private static readonly string[] timeZones = [TimeZone, TimeZone, TimeZone, TimeZone];

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

        GetTimeZonesByIdsQuery query = new(accountIds);
        GetTimeZonesByIdsHandler handler = new(reads);

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
        reads.AllAsync(
            query: new(Pagination: new(1, accountIds.Length), Ids: accountIds),
            track: false,
            ct: ct
        ).Returns(new Result<Account>(
            Count: accountIds.Length,
            Items: [
                CreateAccount(Client, timeZone: timeZones[0]),
                CreateAccount(Contributor, timeZone: timeZones[1]),
                CreateAccount(Designer, timeZone: timeZones[2]),
                CreateAccount(Admin, timeZone: timeZones[3]),
            ]
        ));

        GetTimeZonesByIdsQuery query = new(accountIds);
        GetTimeZonesByIdsHandler handler = new(reads);

        // Act
        string[] actualTimeZones = [.. (await handler.Handle(query, ct)).Select(t => t.TimeZone)];

        // Assert
        Assert.Equal(actualTimeZones, timeZones);
    }
}
