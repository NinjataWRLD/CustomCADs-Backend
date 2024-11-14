using CustomCADs.Account.Domain.Roles.Entities;
using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleCreatedDomainEvent(
    Role Role
) : BaseDomainEvent;
