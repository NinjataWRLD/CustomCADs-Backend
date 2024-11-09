using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.Categories.DomainEvents;

public record CategoryDeletedDomainEvent(
    int Id
) : DomainEvent;
