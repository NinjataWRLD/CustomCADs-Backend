﻿using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Auth.Application.DomainEvents.Email;

public record PasswordResetRequestedEvent(
    string Email, 
    string Endpoint
) : DomainEvent;
