using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.ResetPassword;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ResetPassword;

using static UsersData;

public class ResetUserPasswordHandlerUnitTests : UsersBaseUnitTests
{
	private readonly ResetUserPasswordHandler handler;
	private readonly Mock<IUserService> service = new();

	private const string Token = "email-token";
	private readonly User user = CreateUser();

	public ResetUserPasswordHandlerUnitTests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		ResetUserPasswordCommand command = new(
			Email: user.Email.Value,
			Token: Token,
			NewPassword: MinValidPassword
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.ResetPasswordAsync(user.Email.Value, Token, MinValidPassword), Times.Once());
	}
}
