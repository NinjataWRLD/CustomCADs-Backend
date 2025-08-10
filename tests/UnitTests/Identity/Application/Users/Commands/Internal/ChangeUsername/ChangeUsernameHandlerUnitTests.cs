using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.ChangeUsername;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Identity;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ChangeUsername;

using static UsersData;

public class ChangeUsernameHandlerUnitTests : UsersBaseUnitTests
{
	private readonly ChangeUsernameHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly User user = CreateUser(username: MaxValidUsername);

	public ChangeUsernameHandlerUnitTests()
	{
		handler = new(service.Object, raiser.Object);

		service.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(user);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		ChangeUsernameCommand command = new(
			Username: user.Username,
			NewUsername: MinValidUsername
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.GetByUsernameAsync(MaxValidUsername), Times.Once());
		service.Verify(x => x.UpdateUsernameAsync(user.Id, MinValidUsername), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		ChangeUsernameCommand command = new(
			Username: MaxValidUsername,
			NewUsername: MinValidUsername
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<UserEditedApplicationEvent>(x =>
				x.Username == user.Username
				&& x.Id == user.AccountId
			)
		), Times.Once());
	}
}
