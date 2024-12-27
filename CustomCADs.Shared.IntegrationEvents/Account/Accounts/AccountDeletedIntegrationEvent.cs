namespace CustomCADs.Shared.IntegrationEvents.Account.Accounts;

public record AccountDeletedIntegrationEvent(
    string Username
) : BaseIntegrationEvent;
