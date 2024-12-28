using CustomCADs.Accounts.Application.Accounts.SharedCommandHandlers;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Shared.UseCases.Accounts.Commands;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedCommands;

using static Constants.Roles;
using static Constants.Users;

public class CreateAccountHandlerUnitTests : AccountsBaseUnitTests
{
    private static IWrites<Account> writes;
    private static IUnitOfWork uow;

    [SetUp]
    public void Setup()
    {
        writes = Substitute.For<IWrites<Account>>();
        uow = Substitute.For<IUnitOfWork>();
    }

    [Test]
    [TestCase(Client, ClientUsername, ClientEmail, TimeZone, FirstName, LastName)]
    [TestCase(Contributor, ContributorUsername, ContributorEmail, TimeZone, null, LastName)]
    [TestCase(Designer, DesignerUsername, DesignerEmail, TimeZone, FirstName, null)]
    [TestCase(Admin, AdminUsername, AdminEmail, TimeZone, null, null)]
    public async Task Handle_ShouldCallDatabase(string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            FirstName: firstName,
            LastName: lastName
        );
        CreateAccountHandler handler = new(writes, uow);

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
}
