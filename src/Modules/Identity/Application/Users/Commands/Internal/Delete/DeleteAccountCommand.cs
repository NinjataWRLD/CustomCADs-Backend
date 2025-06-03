namespace CustomCADs.Identity.Application.Users.Commands.Internal.Delete;

public record DeleteAccountCommand(
    string Username
) : ICommand;
