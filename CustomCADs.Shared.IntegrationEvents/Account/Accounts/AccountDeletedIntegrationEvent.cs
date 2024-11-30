using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Accounts;

public record AccountDeletedIntegrationEvent(
    string Username
) : BaseIntegrationEvent;
