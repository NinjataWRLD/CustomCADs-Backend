using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Roles;

public record RoleDeletedIntegrationEvent(
    string Name
) : BaseIntegrationEvent;
