using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Inventory.Domain.Products.DomainEvents;

public record ProductDeletedDomainEvent(
    ProductId Id,
    string ImageKey,
    string CadKey
) : BaseDomainEvent;
