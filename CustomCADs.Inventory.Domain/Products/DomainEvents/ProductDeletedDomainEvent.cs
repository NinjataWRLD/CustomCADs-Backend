using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Inventory.Domain.Products.DomainEvents;

public record ProductDeletedDomainEvent(
    ProductId Id,
    string ImageKey,
    string CadKey
) : BaseDomainEvent;
