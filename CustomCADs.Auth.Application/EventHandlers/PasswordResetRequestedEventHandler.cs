using CustomCADs.Auth.Application.DomainEvents.Email;
using CustomCADs.Shared.Application.Email;

namespace CustomCADs.Auth.Application.EventHandlers;

public class PasswordResetRequestedEventHandler(IEmailService service)
{
    public async Task Handle(PasswordResetRequestedEvent prrEvent)
    {
        await service.SendForgotPasswordEmailAsync(prrEvent.Email, prrEvent.Endpoint).ConfigureAwait(false);
    }
}
