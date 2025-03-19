using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Accounts.Domain.Roles.Events;

public record RoleEditedDomainEvent(
    RoleId Id,
    Role Role
) : BaseDomainEvent;
