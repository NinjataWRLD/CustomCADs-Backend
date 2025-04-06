using CustomCADs.Identity.Domain.Users.Events;
using CustomCADs.Shared.Abstractions.Email;

namespace CustomCADs.Identity.Application.Users.Events.Domain;

public class EmailVerificationRequestedEventHandler(IEmailService service)
{
    public async Task Handle(EmailVerificationRequestedDomainEvent de)
    {
        await service.SendVerificationEmailAsync(de.Email, de.Endpoint).ConfigureAwait(false);
    }
}
