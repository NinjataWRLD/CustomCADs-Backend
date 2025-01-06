using CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers.TimeZone;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetTimeZone.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetTimeZone;

using static AccountsData;

public class GetTimeZoneByIdHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly Mock<IAccountReads> reads = new();

    [Theory]
    [ClassData(typeof(GetTimeZoneByIdValidData))]
    public async Task Handle_ShouldQueryDatabase(AccountId id)
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(CreateAccount());

        GetTimeZoneByIdQuery query = new(id);
        GetTimeZoneByIdHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetTimeZoneByIdValidData))]
    public async Task Handle_ShouldReturnProperly_WhenAccountFound(AccountId id)
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(CreateAccount(timeZone: ValidTimeZone1));

        GetTimeZoneByIdQuery query = new(id);
        GetTimeZoneByIdHandler handler = new(reads.Object);

        // Act
        string actualTimeZone = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(ValidTimeZone1, actualTimeZone);
    }

    [Theory]
    [ClassData(typeof(GetTimeZoneByIdValidData))]
    public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists(AccountId id)
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(null as Account);

        GetTimeZoneByIdQuery query = new(id);
        GetTimeZoneByIdHandler handler = new(reads.Object);

        // Assert
        await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
