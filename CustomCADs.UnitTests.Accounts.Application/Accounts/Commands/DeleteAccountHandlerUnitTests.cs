using CustomCADs.Accounts.Application.Accounts.Commands.Delete;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands;

using static Constants.Roles;
using static Constants.Users;

public class DeleteAccountHandlerUnitTests : AccountsBaseUnitTests
{
    private static IAccountReads reads;
    private static IWrites<Account> writes;
    private static IUnitOfWork uow;
    private static IEventRaiser raiser;

    [SetUp]
    public void Setup()
    {
        reads = Substitute.For<IAccountReads>();
        writes = Substitute.For<IWrites<Account>>();
        uow = Substitute.For<IUnitOfWork>();
        raiser = Substitute.For<IEventRaiser>();
    }

    [Test]
    [TestCase(Client, ClientUsername)]
    [TestCase(Contributor, ContributorUsername)]
    [TestCase(Designer, DesignerUsername)]
    [TestCase(Admin, AdminUsername)]
    public async Task Handle_ShouldCallDatabase(string role, string username)
    {
        // Arrange
        Account account = CreateAccount(role: role, username: username);
        reads.SingleByUsernameAsync(username).Returns(account);

        DeleteAccountCommand command = new(username);
        DeleteAccountHandler handler = new(reads, writes, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Received(1).Remove(account);
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Test]
    [TestCase(Client, ClientUsername)]
    [TestCase(Contributor, ContributorUsername)]
    [TestCase(Designer, DesignerUsername)]
    [TestCase(Admin, AdminUsername)]
    public async Task Handle_ShouldRaiseEvents(string role, string username)
    {
        // Arrange
        Account account = CreateAccount(role: role, username: username);
        reads.SingleByUsernameAsync(username).Returns(account);

        DeleteAccountCommand command = new(username);
        DeleteAccountHandler handler = new(reads, writes, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await raiser.Received(1).RaiseIntegrationEventAsync(
            Arg.Is<AccountDeletedIntegrationEvent>(x => x.Username == username)
        );
    }

    [Test]
    [TestCase(ClientUsername)]
    [TestCase(ContributorUsername)]
    [TestCase(DesignerUsername)]
    [TestCase(AdminUsername)]
    public void Handle_ShouldThrowException_WhenAccountDoesNotExists(string username)
    {
        // Arrange
        reads.SingleByUsernameAsync(username, false, ct).Returns(null as Account);

        DeleteAccountCommand command = new(username);
        DeleteAccountHandler handler = new(reads, writes, uow, raiser);

        // Assert
        Assert.ThrowsAsync<AccountNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
