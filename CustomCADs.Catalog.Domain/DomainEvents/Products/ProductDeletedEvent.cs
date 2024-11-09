using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.DomainEvents.Products;

public record ProductDeletedEvent(Guid Id) : DomainEvent;
