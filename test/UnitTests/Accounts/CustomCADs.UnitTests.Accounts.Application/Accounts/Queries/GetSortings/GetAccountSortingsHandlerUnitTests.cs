using CustomCADs.Accounts.Application.Accounts.Queries.GetSortings;
using CustomCADs.Accounts.Domain.Accounts.Enums;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.GetSortings;

public class GetAccountSortingsHandlerUnitTests : AccountsBaseUnitTests
{
    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Arrange
        GetAccountSortingsQuery query = new();
        GetAccountSortingsHandler handler = new();

        // Act
        string[] sortings = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(sortings, Enum.GetNames<AccountSortingType>());
    }
}
