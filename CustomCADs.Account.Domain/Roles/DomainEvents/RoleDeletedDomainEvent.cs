using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleDeletedDomainEvent(
    RoleId Id,
    string Name
) : BaseDomainEvent;
