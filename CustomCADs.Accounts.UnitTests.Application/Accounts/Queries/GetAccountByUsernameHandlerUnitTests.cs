using CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;
using CustomCADs.Accounts.Domain.Accounts.Reads;

namespace CustomCADs.Accounts.UnitTests.Application.Accounts.Queries;

using static Constants.Roles;
using static Constants.Users;

public class GetAccountByUsernameHandlerUnitTests : AccountsBaseUnitTests
{
    private static IAccountReads reads;

    [SetUp]
    public void Setup()
    {
        reads = Substitute.For<IAccountReads>();

        reads.SingleByUsernameAsync(ClientUsername, track: false, ct)
            .Returns(CreateAccount(role: Client, username: ClientUsername, email: ClientEmail));

        reads.SingleByUsernameAsync(ContributorUsername, track: false, ct)
            .Returns(CreateAccount(role: Contributor, username: ContributorUsername, email: ContributorEmail));

        reads.SingleByUsernameAsync(DesignerUsername, track: false, ct)
            .Returns(CreateAccount(role: Designer, username: DesignerUsername, email: DesignerEmail));

        reads.SingleByUsernameAsync(AdminUsername, track: false, ct)
            .Returns(CreateAccount(role: Admin, username: AdminUsername, email: AdminEmail));

    }

    [Test]
    [TestCase(ClientUsername)]
    [TestCase(ContributorUsername)]
    [TestCase(DesignerUsername)]
    [TestCase(AdminUsername)]
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

    [Test]
    [TestCase(ClientUsername)]
    [TestCase(ContributorUsername)]
    [TestCase(DesignerUsername)]
    [TestCase(AdminUsername)]
    public void Handle_ShouldThrowException_WhenDatabaseMiss(string username)
    {
        // Assert
        reads.SingleByUsernameAsync(username, false, ct).Returns(null as Account);

        GetAccountByUsernameQuery query = new(username);
        GetAccountByUsernameHandler handler = new(reads);

        // Assert
        Assert.ThrowsAsync<AccountNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });

    }
}
