using CustomCADs.Account.Domain.Roles.Entities;
using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleEditedDomainEvent(
    RoleId Id,
    Role Role
) : BaseDomainEvent;
