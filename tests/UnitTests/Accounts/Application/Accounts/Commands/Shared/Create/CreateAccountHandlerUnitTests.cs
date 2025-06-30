using CustomCADs.Accounts.Application.Accounts.Commands.Shared;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.UseCases.Accounts.Commands;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create;

using static Constants;
using static AccountsData;

public class CreateAccountHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly CreateAccountHandler handler;
	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public CreateAccountHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateAccountCommand command = new(
			Role: Roles.Customer,
			Username: ValidUsername,
			Email: ValidEmail1,
			FirstName: ValidFirstName,
			LastName: ValidLastName
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Account>(x =>
				x.RoleName == Roles.Customer
				&& x.Username == ValidUsername
				&& x.Email == ValidEmail1
				&& x.FirstName == ValidFirstName
				&& x.LastName == ValidLastName
			),
			ct
		), Times.Once);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}
}
