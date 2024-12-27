namespace CustomCADs.Shared.IntegrationEvents.Account.Roles;

public record RoleEditedIntegrationEvent(
    string Name,
    string Description
) : BaseIntegrationEvent;
