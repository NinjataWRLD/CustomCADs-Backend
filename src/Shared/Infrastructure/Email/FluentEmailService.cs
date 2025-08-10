using CustomCADs.Shared.Application.Abstractions.Email;
using FluentEmail.Smtp;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CustomCADs.Shared.Infrastructure.Email;

public sealed class FluentEmailService(IOptions<EmailSettings> settings) : IEmailService
{
	private readonly string server = settings.Value.Server;
	private readonly int port = settings.Value.Port;
	private readonly string from = settings.Value.From;
	private readonly string password = settings.Value.Password;

	private SmtpClient Client => new(server, port)
	{
		Credentials = new NetworkCredential(from, password),
		EnableSsl = true,
		DeliveryMethod = SmtpDeliveryMethod.Network,
		UseDefaultCredentials = false
	};

	public async Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default)
	{
		FluentEmail.Core.Email.DefaultSender = new SmtpSender(Client);

		await FluentEmail.Core.Email
			.From(from, name: null)
			.To(to, name: null)
			.Subject(subject)
			.Body(body)
			.SendAsync(ct).ConfigureAwait(false);
	}

	public async Task SendVerificationEmailAsync(string to, string endpoint, CancellationToken ct = default)
	{
		FluentEmail.Core.Email.DefaultSender = new SmtpSender(Client);

		string subject = "Confirm your email at CustomCADs", body = @$"
<h2>Welcome to CustomCADs!</h2>
<p>Please confirm your email by clicking the button below:</p>
<a href='{endpoint}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; border-radius: 5px;'>
	Confirm Email
</a>
";
		await FluentEmail.Core.Email
			.From(from, name: null)
			.To(to, name: null)
			.Subject(subject)
			.Body(body, isHtml: true)
			.SendAsync(ct).ConfigureAwait(false);
	}

	public async Task SendForgotPasswordEmailAsync(string to, string endpoint, CancellationToken ct = default)
	{
		FluentEmail.Core.Email.DefaultSender = new SmtpSender(Client);

		string subject = "Forgot your CustomCADs password?", body = @$"
<h2>We've got you!</h2>
<h6>Click this button to set a new password.</h6>
<a href='{endpoint}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; border-radius: 5px;'>
	Reset Password
</a>
";
		await FluentEmail.Core.Email
			.From(from, name: null)
			.To(to, name: null)
			.Subject(subject)
			.Body(body, isHtml: true)
			.SendAsync(ct).ConfigureAwait(false);
	}

	public async Task SendRewardGrantedEmailAsync(string to, string url, CancellationToken ct = default)
	{
		FluentEmail.Core.Email.DefaultSender = new SmtpSender(Client);

		string subject = "Payment successful - reward granted!", body = @$"
<h2>The 3D Models are yours to enjoy now!</h2>
<h6>You can follow this link to visit them:</h6>
<a href='{url}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; border-radius: 5px;'>
	Check Reward out
</a>
";
		await FluentEmail.Core.Email
			.From(from, name: null)
			.To(to, name: null)
			.Subject(subject)
			.Body(body, isHtml: true)
			.SendAsync(ct).ConfigureAwait(false);
	}
}
