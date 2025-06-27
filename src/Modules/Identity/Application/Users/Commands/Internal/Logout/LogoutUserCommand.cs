namespace CustomCADs.Identity.Application.Users.Commands.Internal.Logout;

public record LogoutUserCommand(
	string Username,
	string? RefreshToken
) : ICommand;
