using CustomCADs.Accounts.Application.Accounts.Commands.Shared;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Shared.UseCases.Accounts.Commands;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create;

using CustomCADs.Accounts.Domain.Repositories.Writes;
using Data;

public class CreateAccountHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly CreateAccountHandler handler;
	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public CreateAccountHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);
	}

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
