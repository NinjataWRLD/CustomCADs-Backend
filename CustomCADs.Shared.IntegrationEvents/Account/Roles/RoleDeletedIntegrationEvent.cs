using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Roles;

public record RoleDeletedIntegrationEvent(
    string Name
) : BaseIntegrationEvent;
