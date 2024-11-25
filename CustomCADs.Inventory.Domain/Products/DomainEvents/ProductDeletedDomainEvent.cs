using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Inventory.Domain.Products.DomainEvents;

public record ProductDeletedDomainEvent(
    ProductId Id,
    string ImageKey,
    string CadKey
) : BaseDomainEvent;
