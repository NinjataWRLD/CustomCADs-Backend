﻿using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Shared.IntegrationEvents.Auth;

public record UserRegisteredIntegrationEvent(
    string Role,
    string Username,
    string Email,
    string TimeZone,
    string? FirstName = default,
    string? LastName = default
) : BaseIntegrationEvent;
