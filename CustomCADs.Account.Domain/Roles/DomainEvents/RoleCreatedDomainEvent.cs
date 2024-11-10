using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleCreatedDomainEvent(
    Role Role
) : BaseDomainEvent;
