using CustomCADs.Identity.Domain.Users.Events;
using CustomCADs.Shared.Abstractions.Email;

namespace CustomCADs.Identity.Application.Users.EventHandlers.Domain;

public class PasswordResetRequestedEventHandler(IEmailService service)
{
    public async Task Handle(PasswordResetRequestedDomainEvent de)
    {
        await service.SendForgotPasswordEmailAsync(de.Email, de.Endpoint).ConfigureAwait(false);
    }
}
