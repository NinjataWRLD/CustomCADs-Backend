namespace CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;

public record VerificationEmailCommand(
    string Username,
    Func<string, string> GetUri
) : ICommand;
