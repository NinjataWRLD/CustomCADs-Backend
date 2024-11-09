using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductDeletedDomainEvent(
    Guid Id
) : DomainEvent;
