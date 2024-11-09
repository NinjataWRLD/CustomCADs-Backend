﻿using CustomCADs.Auth.Application.DomainEvents.Email;
using CustomCADs.Shared.Application.Email;

namespace CustomCADs.Auth.Application.EventHandlers;

public class EmailVerificationRequestedEventHandler(IEmailService service)
{
    public async Task Handle(EmailVerificationRequestedDomainEvent de)
    {
        await service.SendVerificationEmailAsync(de.Email, de.Endpoint).ConfigureAwait(false);
    }
}
