using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.Categories.DomainEvents;

public record CategoryDeletedDomainEvent(
    int Id
) : DomainEvent;
