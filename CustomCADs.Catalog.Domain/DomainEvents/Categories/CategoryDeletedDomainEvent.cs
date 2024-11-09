using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.DomainEvents.Categories;

public record CategoryDeletedDomainEvent(int Id) : DomainEvent;
