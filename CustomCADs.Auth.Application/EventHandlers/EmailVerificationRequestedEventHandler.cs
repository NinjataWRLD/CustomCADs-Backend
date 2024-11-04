using CustomCADs.Shared.Core.Email;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Auth.Application.EventHandlers;

public class EmailVerificationRequestedEventHandler(IEmailService service)
{
    public async Task Handle(EmailVerificationRequestedEvent e, CancellationToken ct = default)
    {
        await service.SendVerificationEmailAsync(e.Email, e.Endpoint).ConfigureAwait(false);
    }
}
