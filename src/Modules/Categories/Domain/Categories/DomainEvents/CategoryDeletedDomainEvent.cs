using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Categories.Domain.Categories.DomainEvents;

public record CategoryDeletedDomainEvent(
    CategoryId Id
) : BaseDomainEvent;
