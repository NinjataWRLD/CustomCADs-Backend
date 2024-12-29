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
    private readonly IAccountReads reads = Substitute.For<IAccountReads>();
    private readonly IWrites<Account> writes = Substitute.For<IWrites<Account>>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly IEventRaiser raiser = Substitute.For<IEventRaiser>();

    [Theory]
    [InlineData(Client, ClientUsername)]
    [InlineData(Contributor, ContributorUsername)]
    [InlineData(Designer, DesignerUsername)]
    [InlineData(Admin, AdminUsername)]
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

    [Theory]
    [InlineData(Client, ClientUsername)]
    [InlineData(Contributor, ContributorUsername)]
    [InlineData(Designer, DesignerUsername)]
    [InlineData(Admin, AdminUsername)]
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

    [Theory]
    [InlineData(ClientUsername)]
    [InlineData(ContributorUsername)]
    [InlineData(DesignerUsername)]
    [InlineData(AdminUsername)]
    public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists(string username)
    {
        // Arrange
        reads.SingleByUsernameAsync(username, false, ct).Returns(null as Account);

        DeleteAccountCommand command = new(username);
        DeleteAccountHandler handler = new(reads, writes, uow, raiser);

        // Assert
        await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
