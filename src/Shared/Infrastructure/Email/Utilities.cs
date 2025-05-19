using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CustomCADs.Shared.Infrastructure.Email;

public static class Utilities
{
	public static async Task SendMessageAsync(this SmtpClient client, string server, int port, SecureSocketOptions options, string from, string password, MimeMessage message, CancellationToken ct = default)
	{
		await client.ConnectAsync(server, port, options, ct).ConfigureAwait(false);
		await client.AuthenticateAsync(from, password, ct).ConfigureAwait(false);
		await client.SendAsync(message, ct).ConfigureAwait(false);
		await client.DisconnectAsync(quit: true, ct).ConfigureAwait(false);
	}
}
