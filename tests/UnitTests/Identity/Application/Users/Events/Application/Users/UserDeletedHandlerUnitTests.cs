using CustomCADs.Identity.Application.Users.Events.Application.Users;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Users;

using static UsersData;

public class UserDeletedHandlerUnitTests : UsersBaseUnitTests
{
	private readonly UserDeletedHandler handler;
	private readonly Mock<IUserWrites> writes = new();

	public UserDeletedHandlerUnitTests()
	{
		handler = new(writes.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		AccountDeletedApplicationEvent ae = new(MaxValidUsername);

		// Act
		await handler.Handle(ae);

		// Assert
		writes.Verify(x => x.DeleteAsync(MaxValidUsername), Times.Once());
	}
}
