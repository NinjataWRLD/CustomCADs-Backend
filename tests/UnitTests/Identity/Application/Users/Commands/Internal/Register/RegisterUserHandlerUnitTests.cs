using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.Register;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Commands;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Register;

using static UsersData;

public class RegisterUserHandlerUnitTests : UsersBaseUnitTests
{
	private readonly RegisterUserHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<IRequestSender> sender = new();

	public RegisterUserHandlerUnitTests()
	{
		handler = new(service.Object, sender.Object);

		sender.Setup(x => x.SendCommandAsync(
			It.Is<CreateAccountCommand>(x => x.Username == MaxValidUsername),
			ct
		)).ReturnsAsync(ValidAccountId);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		RegisterUserCommand command = new(
			Role: ValidRole,
			Username: MaxValidUsername,
			Email: ValidEmail,
			Password: MinValidPassword,
			FirstName: null,
			LastName: null
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.CreateAsync(
			It.Is<User>(x =>
				x.Username == command.Username
				&& x.AccountId == ValidAccountId
			),
			command.Password
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		RegisterUserCommand command = new(
			Role: ValidRole,
			Username: MaxValidUsername,
			Email: ValidEmail,
			Password: MinValidPassword,
			FirstName: null,
			LastName: null
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendCommandAsync(
			It.Is<CreateAccountCommand>(x =>
				x.Role == command.Role
				&& x.Username == command.Username
				&& x.Email == command.Email
				&& x.FirstName == command.FirstName
				&& x.LastName == command.LastName
			),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenRegisterUnsuccessfuly()
	{
		// Arrange
		service.Setup(x => x.CreateAsync(
			It.Is<User>(x => x.Username == MaxValidUsername),
			MinValidPassword
		)).ThrowsAsync(new CustomException("CreationErrorMessage"));

		RegisterUserCommand command = new(
			Role: ValidRole,
			Username: MaxValidUsername,
			Email: ValidEmail,
			Password: MinValidPassword,
			FirstName: null,
			LastName: null
		);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
