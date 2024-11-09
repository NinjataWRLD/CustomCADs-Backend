using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleCreatedDomainEvent(
    Role Role
) : DomainEvent;
