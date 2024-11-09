using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.DomainEvents.Categories;

public record CategoryDeletedEvent(int Id) : DomainEvent;
