using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Accounts;

public record AccountCreatedIntegrationEvent(
    AccountId Id,
    string Role,
    string Username,
    string Email,
    string Password
) : BaseIntegrationEvent;
