using CustomCADs.Accounts.Application.Accounts.Commands.Create;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.IntegrationEvents.Account.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands;

using static Constants.Roles;
using static Constants.Users;

public class CreateAccountHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly IWrites<Account> writes = Substitute.For<IWrites<Account>>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly IEventRaiser raiser = Substitute.For<IEventRaiser>();


    [Theory]
    [InlineData(Client, ClientUsername, ClientEmail, TimeZone, Password, null, null)]
    [InlineData(Contributor, ContributorUsername, ContributorEmail, TimeZone, Password, null, null)]
    [InlineData(Designer, DesignerUsername, DesignerEmail, TimeZone, Password, null, null)]
    [InlineData(Admin, AdminUsername, AdminEmail, TimeZone, Password, null, null)]
    public async Task Handle_ShouldCallDatabase(string role, string username, string email, string timeZone, string password, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            Password: password,
            FirstName: firstName,
            LastName: lastName
        );
        CreateAccountHandler handler = new(writes, uow, raiser);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await writes.Received(1).AddAsync(
            Arg.Is<Account>(x =>
                x.RoleName == role
                && x.Username == username
                && x.Email == email
                && x.TimeZone == timeZone
                && x.FirstName == firstName
                && x.LastName == lastName
            ),
            ct: ct
        );
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Theory]
    [InlineData(Client, ClientUsername, ClientEmail, TimeZone, Password, null, null)]
    [InlineData(Contributor, ContributorUsername, ContributorEmail, TimeZone, Password, null, null)]
    [InlineData(Designer, DesignerUsername, DesignerEmail, TimeZone, Password, null, null)]
    [InlineData(Admin, AdminUsername, AdminEmail, TimeZone, Password, null, null)]
    public async Task Handle_ShouldRaiseEvents(string role, string username, string email, string timeZone, string password, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            Password: password,
            FirstName: firstName,
            LastName: lastName
        );
        CreateAccountHandler handler = new(writes, uow, raiser);

        // Act
        AccountId id = await handler.Handle(command, CancellationToken.None);

        // Assert
        await raiser.Received(1).RaiseIntegrationEventAsync(
            Arg.Is<AccountCreatedIntegrationEvent>(x =>
                x.Id == id
                && x.Username == username
                && x.Email == email
                && x.Password == password)
        );
    }
}
