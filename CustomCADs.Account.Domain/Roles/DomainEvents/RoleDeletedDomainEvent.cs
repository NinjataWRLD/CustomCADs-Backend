using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleDeletedDomainEvent(
    RoleId Id,
    string Name
) : BaseDomainEvent;
