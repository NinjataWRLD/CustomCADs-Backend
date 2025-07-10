namespace CustomCADs.Identity.Application.Users.Commands.Internal.Delete;

public record DeleteUserCommand(
	string Username
) : ICommand;
