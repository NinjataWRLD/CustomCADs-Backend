using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.Delete;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Identity;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Delete;

using static UsersData;

public class DeleteUserHandlerUnitTests : UsersBaseUnitTests
{
	private readonly DeleteUserHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly User user = CreateUser(username: MaxValidUsername);

	public DeleteUserHandlerUnitTests()
	{
		handler = new(service.Object, raiser.Object);

		service.Setup(x => x.GetAccountIdAsync(user.Username)).ReturnsAsync(ValidAccountId);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		DeleteUserCommand command = new(user.Username);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.GetAccountIdAsync(MaxValidUsername), Times.Once());
		service.Verify(x => x.DeleteAsync(user.Username), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		DeleteUserCommand command = new(user.Username);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<UserDeletedApplicationEvent>(x => x.Id == user.AccountId)
		), Times.Once());
	}
}
