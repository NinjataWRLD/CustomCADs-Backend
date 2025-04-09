using CustomCADs.Accounts.Application.Accounts.Commands.Shared;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Shared.UseCases.Accounts.Commands;
using CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create;

public class CreateAccountHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly Mock<IWrites<Account>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();

    [Theory]
    [ClassData(typeof(CreateAccountValidData))]
    public async Task Handle_ShouldPersistToDatabase(string role, string username, string email, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            FirstName: firstName,
            LastName: lastName
        );
        CreateAccountHandler handler = new(writes.Object, uow.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<Account>(x =>
                x.RoleName == role
                && x.Username == username
                && x.Email == email
                && x.FirstName == firstName
                && x.LastName == lastName
            ),
            ct
        ), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }
}
