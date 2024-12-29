using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.TimeZone;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetTimeZone.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetTimeZone;

using static AccountsData;

public class GetTimeZoneByIdHandlerData : TheoryData<AccountId>;

public class GetTimeZoneByIdHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly IAccountReads reads = Substitute.For<IAccountReads>();

    [Theory]
    [ClassData(typeof(GetTimeZoneByIdHandlerValidData))]
    public async Task Handle_CallsDatabase(AccountId id)
    {
        // Arrange
        reads.SingleByIdAsync(id, false, ct).Returns(CreateAccount());

        GetTimeZoneByIdQuery query = new(id);
        GetTimeZoneByIdHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByIdAsync(id, false, ct);
    }

    [Theory]
    [ClassData(typeof(GetTimeZoneByIdHandlerValidData))]
    public async Task Handle_ShouldReturnProperly_WhenAccountExists(AccountId id)
    {
        // Arrange
        reads.SingleByIdAsync(id, false, ct).Returns(CreateAccount(timeZone: ValidTimeZone1));

        GetTimeZoneByIdQuery query = new(id);
        GetTimeZoneByIdHandler handler = new(reads);

        // Act
        string actualTimeZone = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(ValidTimeZone1, actualTimeZone);
    }

    [Theory]
    [ClassData(typeof(GetTimeZoneByIdHandlerValidData))]
    public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists(AccountId id)
    {
        // Arrange
        reads.SingleByIdAsync(id, false, ct).Returns(null as Account);

        GetTimeZoneByIdQuery query = new(id);
        GetTimeZoneByIdHandler handler = new(reads);

        // Assert
        await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
