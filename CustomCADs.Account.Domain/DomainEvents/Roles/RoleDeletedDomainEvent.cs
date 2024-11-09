using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Account.Domain.DomainEvents.Roles;

public record RoleDeletedDomainEvent(
    int Id,
    string Name
) : DomainEvent;
