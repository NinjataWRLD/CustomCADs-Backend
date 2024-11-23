using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Domain.Products.DomainEvents;

public record ProductDeletedDomainEvent(
    ProductId Id,
    string ImageKey,
    string CadKey
) : BaseDomainEvent;
