namespace CustomCADs.Identity.Application.Users.Commands.Internal.Logout;

public record LogoutUserCommand(
	string? RefreshToken
) : ICommand;
