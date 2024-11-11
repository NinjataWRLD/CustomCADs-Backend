using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductDeletedDomainEvent(
    ProductId Id
) : BaseDomainEvent;
