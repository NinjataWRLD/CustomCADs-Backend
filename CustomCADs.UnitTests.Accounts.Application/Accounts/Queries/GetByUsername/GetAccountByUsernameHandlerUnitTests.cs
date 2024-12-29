using CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.GetByUsername.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.GetByUsername;

public class GetAccountByUsernameHandlerData : TheoryData<string>;

public class GetAccountByUsernameHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly IAccountReads reads = Substitute.For<IAccountReads>();

    public GetAccountByUsernameHandlerUnitTests()
    {
        reads.SingleByUsernameAsync(Arg.Any<string>(), false, ct).Returns(CreateAccount());
    }

    [Theory]
    [ClassData(typeof(GetAccountByUsernameHandlerValidData))]
    public async Task Handle_ShouldCallDatabase(string username)
    {
        // Assert
        GetAccountByUsernameQuery query = new(username);
        GetAccountByUsernameHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByUsernameAsync(username, track: false, ct);
    }

    [Theory]
    [ClassData(typeof(GetAccountByUsernameHandlerValidData))]
    public async Task Handle_ShouldThrowException_WhenDatabaseMiss(string username)
    {
        // Assert
        reads.SingleByUsernameAsync(username, false, ct).Returns(null as Account);

        GetAccountByUsernameQuery query = new(username);
        GetAccountByUsernameHandler handler = new(reads);

        // Assert
        await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });

    }
}
