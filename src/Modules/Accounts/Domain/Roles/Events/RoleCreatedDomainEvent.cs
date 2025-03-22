using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Accounts.Domain.Roles.Events;

public record RoleCreatedDomainEvent(
    Role Role
) : BaseDomainEvent;
