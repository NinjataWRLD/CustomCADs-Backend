using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.DomainEvents.Products;

public record ProductDeletedDomainEvent(Guid Id) : DomainEvent;
