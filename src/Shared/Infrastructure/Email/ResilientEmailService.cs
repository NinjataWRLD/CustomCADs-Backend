using CustomCADs.Shared.Application.Abstractions.Email;

namespace CustomCADs.Shared.Infrastructure.Email;

public class ResilientEmailService(
	IEmailService inner,
	Polly.IAsyncPolicy policy
) : IEmailService
{
	public Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.SendEmailAsync(to, subject, body, ct));

	public Task SendForgotPasswordEmailAsync(string to, string endpoint, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.SendForgotPasswordEmailAsync(to, endpoint, ct));

	public Task SendRewardGrantedEmailAsync(string to, string url, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.SendRewardGrantedEmailAsync(to, url, ct));

	public Task SendVerificationEmailAsync(string to, string endpoint, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.SendVerificationEmailAsync(to, endpoint, ct));
}
