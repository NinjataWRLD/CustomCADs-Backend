using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleDeletedDomainEvent(
    RoleId Id,
    string Name
) : BaseDomainEvent;
