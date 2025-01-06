using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.TimeZone;
using CustomCADs.Accounts.Application.Roles.Commands.Create;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetTimeZones.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetTimeZones;

using static AccountsData;

public class GetTimeZonesByIdsHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly IAccountReads reads = Substitute.For<IAccountReads>();
    private static readonly string[] timeZones = [
        ValidTimeZone1, 
        ValidTimeZone2,
        ValidTimeZone1, 
        ValidTimeZone2,
    ];

    [Theory]
    [ClassData(typeof(GetTimeZonesByIdsValidData))]
    public async Task Handle_ShouldQueryDatabase(params AccountId[] ids)
    {
        // Arrange
        AccountQuery accountQuery = new(Pagination: new(1, ids.Length), Ids: ids);
        reads.AllAsync(accountQuery, false, ct).Returns(new Result<Account>(
            Count: ids.Length,
            Items: [.. ids.Select(id => CreateAccount())]
        ));

        GetTimeZonesByIdsQuery query = new(ids);
        GetTimeZonesByIdsHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).AllAsync(accountQuery, false, ct);
    }

    [Theory]
    [ClassData(typeof(GetTimeZonesByIdsValidData))]
    public async Task Handle_ShouldReturnProperly(AccountId[] ids)
    {
        // Arrange
        AccountQuery accountQuery = new(Pagination: new(1, ids.Length), Ids: ids);
        reads.AllAsync(accountQuery, false, ct).Returns(new Result<Account>(
            Count: ids.Length,
            Items: [.. 
                Enumerable.Range(0, ids.Length).Select(i => 
                    CreateAccount(timeZone: timeZones[i]))
            ]
        ));

        GetTimeZonesByIdsQuery query = new(ids);
        GetTimeZonesByIdsHandler handler = new(reads);

        // Act
        string[] actualTimeZones = [.. (await handler.Handle(query, ct)).Select(t => t.TimeZone)];

        // Assert
        Assert.Equal(actualTimeZones, timeZones);
    }
}
