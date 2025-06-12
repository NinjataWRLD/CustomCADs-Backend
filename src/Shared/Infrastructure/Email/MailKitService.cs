using CustomCADs.Shared.Abstractions.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CustomCADs.Shared.Infrastructure.Email;

public sealed class MailKitService(IOptions<EmailSettings> settings) : IEmailService
{
	private const SecureSocketOptions Options = SecureSocketOptions.StartTls;
	private readonly string server = settings.Value.Server;
	private readonly int port = settings.Value.Port;
	private readonly string from = settings.Value.From;
	private readonly string password = settings.Value.Password;

	public async Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default)
	{
		try
		{
			MailboxAddress toEmail = new("", to), fromEmail = new("", from);

			MimeMessage message = new()
			{
				Subject = subject,
				Body = new TextPart("plain") { Text = body },
			};
			message.From.Add(fromEmail);
			message.To.Add(toEmail);

			using SmtpClient client = new();
			await client.SendMessageAsync(server, port, Options, from, password, message, ct).ConfigureAwait(false);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task SendVerificationEmailAsync(string to, string endpoint, CancellationToken ct = default)
	{
		try
		{
			MailboxAddress toEmail = new("", to), fromEmail = new("", from);
			string html = @$"
<h2>Welcome to CustomCADs!</h2>
<p>Please confirm your email by clicking the button below:</p>
<a href='{endpoint}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; border-radius: 5px;'>Confirm Email</a>
";
			MimeMessage message = new()
			{
				Subject = "Confirm your email at CustomCADs",
				Body = new BodyBuilder() { HtmlBody = html }.ToMessageBody()
			};
			message.From.Add(fromEmail);
			message.To.Add(toEmail);

			using SmtpClient client = new();
			await client.SendMessageAsync(server, port, Options, from, password, message, ct: ct).ConfigureAwait(false);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task SendForgotPasswordEmailAsync(string to, string endpoint, CancellationToken ct = default)
	{
		try
		{
			MailboxAddress toEmail = new("", to), fromEmail = new("", from);

			string html = @$"
<h2>We've got you!</h2>
<h6>Click this button to set a new password.</h6>
<a href='{endpoint}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; border-radius: 5px;'>Reset Password</a>
";

			MimeMessage message = new()
			{
				Subject = "Forgot your CustomCADs password?",
				Body = new BodyBuilder() { HtmlBody = html }.ToMessageBody()
			};
			message.From.Add(fromEmail);
			message.To.Add(toEmail);

			using SmtpClient client = new();
			await client.SendMessageAsync(server, port, Options, from, password, message, ct: ct).ConfigureAwait(false);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task SendRewardGrantedEmailAsync(string to, string url, CancellationToken ct = default)
	{
		try
		{
			MailboxAddress toEmail = new("", to), fromEmail = new("", from);

			string html = @$"
<h2>The 3D Models are yours to ejoy now!</h2>
<h6>You can follow this link to visit them:</h6>
<a href='{url}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; border-radius: 5px;'>Reset Password</a>
";

			MimeMessage message = new()
			{
				Subject = "Payment successful - reward granted!",
				Body = new BodyBuilder() { HtmlBody = html }.ToMessageBody()
			};
			message.From.Add(fromEmail);
			message.To.Add(toEmail);

			using SmtpClient client = new();
			await client.SendMessageAsync(server, port, Options, from, password, message, ct: ct).ConfigureAwait(false);
		}
		catch (Exception)
		{
			throw;
		}
	}
}
