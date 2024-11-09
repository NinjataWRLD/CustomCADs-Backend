using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.DomainEvents.Categories;

public record CategoryEditedDomainEvent(int Id, Category Category) : DomainEvent;