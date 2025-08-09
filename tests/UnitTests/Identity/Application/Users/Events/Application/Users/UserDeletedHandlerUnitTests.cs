using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Events.Application.Users;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Users;

using static UsersData;

public class UserDeletedHandlerUnitTests : UsersBaseUnitTests
{
	private readonly UserDeletedHandler handler;
	private readonly Mock<IUserService> service = new();

	public UserDeletedHandlerUnitTests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		AccountDeletedApplicationEvent ae = new(MaxValidUsername);

		// Act
		await handler.Handle(ae);

		// Assert
		service.Verify(x => x.DeleteAsync(MaxValidUsername), Times.Once());
	}
}
