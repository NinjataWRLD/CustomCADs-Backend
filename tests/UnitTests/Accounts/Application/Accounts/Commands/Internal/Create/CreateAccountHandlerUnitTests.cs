using CustomCADs.Accounts.Application.Accounts.Commands.Internal.Create;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Create;

using static AccountsData;
using static Constants;

public class CreateAccountHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly CreateAccountHandler handler;
	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IEventRaiser> raiser = new();

	public CreateAccountHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, raiser.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateAccountCommand command = new(
			Role: Roles.Customer,
			Username: ValidUsername,
			Email: ValidEmail1,
			Password: ValidPassword,
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
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		CreateAccountCommand command = new(
			Role: Roles.Customer,
			Username: ValidUsername,
			Email: ValidEmail1,
			Password: ValidPassword,
			FirstName: ValidFirstName,
			LastName: ValidLastName
		);

		// Act
		AccountId id = await handler.Handle(command, CancellationToken.None);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<AccountCreatedApplicationEvent>(x =>
				x.Id == id
				&& x.Username == ValidUsername
				&& x.Email == ValidEmail1
				&& x.Password == ValidPassword
			)
		), Times.Once());
	}
}
