using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Account.Domain.DomainEvents.Roles;

public record RoleDeletedEvent(int Id, string Name) : DomainEvent;
