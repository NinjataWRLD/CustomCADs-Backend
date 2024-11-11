using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Roles;

public record RoleCreatedIntegrationEvent(
    string Name,
    string Description
) : BaseIntegrationEvent;
