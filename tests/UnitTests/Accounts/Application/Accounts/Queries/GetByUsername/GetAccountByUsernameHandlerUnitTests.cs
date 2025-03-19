using CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.GetByUsername.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.GetByUsername;

public class GetAccountByUsernameHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly Mock<IAccountReads> reads = new();

    public GetAccountByUsernameHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByUsernameAsync(It.IsAny<string>(), false, ct)).ReturnsAsync(CreateAccount());
    }

    [Theory]
    [ClassData(typeof(GetAccountByUsernameValidData))]
    public async Task Handle_ShouldQueryDatabase(string username)
    {
        // Arrange
        GetAccountByUsernameQuery query = new(username);
        GetAccountByUsernameHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByUsernameAsync(username, false, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetAccountByUsernameValidData))]
    public async Task Handle_ShouldThrowException_WhenDatabaseMiss(string username)
    {
        // Arrange
        reads.Setup(x => x.SingleByUsernameAsync(username, false, ct)).ReturnsAsync(null as Account);

        GetAccountByUsernameQuery query = new(username);
        GetAccountByUsernameHandler handler = new(reads.Object);

        // Assert
        await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
