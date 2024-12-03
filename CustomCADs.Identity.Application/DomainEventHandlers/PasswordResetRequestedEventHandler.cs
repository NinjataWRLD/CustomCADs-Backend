using CustomCADs.Identity.Domain.DomainEvents.Email;
using CustomCADs.Shared.Application.Email;

namespace CustomCADs.Identity.Application.DomainEventHandlers;

public class PasswordResetRequestedEventHandler(IEmailService service)
{
    public async Task Handle(PasswordResetRequestedDomainEvent de)
    {
        await service.SendForgotPasswordEmailAsync(de.Email, de.Endpoint).ConfigureAwait(false);
    }
}
