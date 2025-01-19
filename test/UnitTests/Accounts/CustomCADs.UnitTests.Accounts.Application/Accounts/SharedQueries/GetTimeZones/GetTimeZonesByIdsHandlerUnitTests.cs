using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.TimeZone;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetTimeZones.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetTimeZones;

using static AccountsData;

public class GetTimeZonesByIdsHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly Mock<IAccountReads> reads = new();
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
        reads.Setup(x => x.AllAsync(accountQuery, false, ct)).ReturnsAsync(new Result<Account>(
            Count: ids.Length,
            Items: [.. ids.Select(id => CreateAccountWithId(id))]
        ));

        GetTimeZonesByIdsQuery query = new(ids);
        GetTimeZonesByIdsHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.AllAsync(accountQuery, false, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetTimeZonesByIdsValidData))]
    public async Task Handle_ShouldReturnProperly(AccountId[] ids)
    {
        // Arrange
        AccountQuery accountQuery = new(Pagination: new(1, ids.Length), Ids: ids);
        reads.Setup(x => x.AllAsync(accountQuery, false, ct)).ReturnsAsync(new Result<Account>(
            Count: ids.Length,
            Items: [..
                Enumerable.Range(0, ids.Length).Select(i =>
                    CreateAccountWithId(id: ids[i], timeZone: timeZones[i]))
            ]
        ));

        GetTimeZonesByIdsQuery query = new(ids);
        GetTimeZonesByIdsHandler handler = new(reads.Object);

        // Act
        string[] actualTimeZones = [.. (await handler.Handle(query, ct)).Select(t => t.Value)];

        // Assert
        Assert.Equal(actualTimeZones, timeZones);
    }
}
