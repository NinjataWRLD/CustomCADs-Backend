namespace CustomCADs.Identity.Application.Users.Commands.Internal.ChangeUsername;

public record ChangeUsernameCommand(
    string Username,
    string NewUsername
) : ICommand;
