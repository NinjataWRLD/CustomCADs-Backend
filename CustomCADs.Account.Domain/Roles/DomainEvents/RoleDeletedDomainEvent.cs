using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleDeletedDomainEvent(
    int Id,
    string Name
) : DomainEvent;
