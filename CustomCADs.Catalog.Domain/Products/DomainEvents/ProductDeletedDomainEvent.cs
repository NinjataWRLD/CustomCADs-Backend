using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductDeletedDomainEvent(
    ProductId Id
) : BaseDomainEvent;
