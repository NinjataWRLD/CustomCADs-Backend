using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetSortings;
using CustomCADs.Accounts.Domain.Accounts.Enums;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Internal.GetSortings;

public class GetAccountSortingsHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly GetAccountSortingsHandler handler = new();

    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Arrange
        GetAccountSortingsQuery query = new();

        // Act
        string[] sortings = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(sortings, Enum.GetNames<AccountSortingType>());
    }
}
