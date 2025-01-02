using CustomCADs.Accounts.Application.Accounts.SharedCommandHandlers;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Shared.UseCases.Accounts.Commands;
using CustomCADs.UnitTests.Accounts.Application.Accounts.SharedCommands.Create.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedCommands.Create;

public class CreateAccountHandlerData : TheoryData<string, string, string, string, string?, string?>;

public class CreateAccountHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly IWrites<Account> writes = Substitute.For<IWrites<Account>>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();

    [Theory]
    [ClassData(typeof(CreateAccountHandlerValidData))]
    public async Task Handle_ShouldPersistToDatabase(string role, string username, string email, string timeZone, string? firstName, string? lastName)
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
