namespace CustomCADs.Shared.Abstractions.Email;

public interface IEmailService
{
	Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default);
	Task SendVerificationEmailAsync(string to, string endpoint, CancellationToken ct = default);
	Task SendForgotPasswordEmailAsync(string to, string endpoint, CancellationToken ct = default);
}
