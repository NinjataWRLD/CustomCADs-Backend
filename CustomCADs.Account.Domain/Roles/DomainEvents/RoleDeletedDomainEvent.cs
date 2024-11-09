using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleDeletedDomainEvent(
    int Id,
    string Name
) : DomainEvent;
