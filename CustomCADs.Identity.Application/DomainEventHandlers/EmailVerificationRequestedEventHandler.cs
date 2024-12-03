using CustomCADs.Identity.Domain.DomainEvents.Email;
using CustomCADs.Shared.Application.Email;

namespace CustomCADs.Identity.Application.DomainEventHandlers;

public class EmailVerificationRequestedEventHandler(IEmailService service)
{
    public async Task Handle(EmailVerificationRequestedDomainEvent de)
    {
        await service.SendVerificationEmailAsync(de.Email, de.Endpoint).ConfigureAwait(false);
    }
}
