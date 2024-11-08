using CustomCADs.Shared.Application.Email;
using CustomCADs.Shared.Core.Events.Email;

namespace CustomCADs.Auth.Application.EventHandlers;

public class PasswordResetRequestedEventHandler(IEmailService service)
{
    public async Task Handle(PasswordResetRequestedEvent prrEvent)
    {
        await service.SendForgotPasswordEmailAsync(prrEvent.Email, prrEvent.Endpoint).ConfigureAwait(false);
    }
}
