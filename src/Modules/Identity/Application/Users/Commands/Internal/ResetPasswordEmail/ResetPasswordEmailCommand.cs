namespace CustomCADs.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;

public record ResetPasswordEmailCommand(
    string Email
) : ICommand;
