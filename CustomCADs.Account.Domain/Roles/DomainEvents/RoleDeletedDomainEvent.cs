using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleDeletedDomainEvent(
    RoleId Id,
    string Name
) : BaseDomainEvent;
