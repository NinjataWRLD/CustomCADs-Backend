using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Events.Application.Users;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Users;

using static UsersData;

public class UserCreatedHandlerUnitTests : UsersBaseUnitTests
{
	private readonly UserCreatedHandler handler;
	private readonly Mock<IUserService> service = new();

	private readonly User user = CreateUser();

	public UserCreatedHandlerUnitTests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		AccountCreatedApplicationEvent ae = new(
			Id: user.AccountId,
			Role: user.Role,
			Username: user.Username,
			Email: user.Email.Value,
			Password: MinValidPassword
		);

		// Act
		await handler.Handle(ae);

		// Assert
		service.Verify(x => x.CreateAsync(
			It.Is<User>(x =>
				x.Role == ae.Role
				&& x.Username == ae.Username
				&& x.Email.Value == ae.Email
				&& x.AccountId == ae.Id
			),
			ae.Password
		), Times.Once());
	}
}
