namespace CustomCADs.Identity.Application.Users.Commands.Internal.ResetPassword;

public record ResetUserPasswordCommand(
	string Email,
	string Token,
	string NewPassword
) : ICommand;
