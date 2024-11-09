using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleEditedDomainEvent(
    int Id,
    Role Role
) : DomainEvent;
