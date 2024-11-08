using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.DomainEvents.Products;

public record ProductDeletedEvent(Guid Id) : IEvent;
