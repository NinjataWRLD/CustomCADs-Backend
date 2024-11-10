using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleEditedDomainEvent(
    int Id,
    Role Role
) : DomainEvent;
