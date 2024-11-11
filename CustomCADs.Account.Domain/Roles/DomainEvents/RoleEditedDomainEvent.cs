using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleEditedDomainEvent(
    RoleId Id,
    Role Role
) : BaseDomainEvent;
