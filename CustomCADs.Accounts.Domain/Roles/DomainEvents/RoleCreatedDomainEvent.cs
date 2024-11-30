using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Accounts.Domain.Roles.DomainEvents;

public record RoleCreatedDomainEvent(
    Role Role
) : BaseDomainEvent;
