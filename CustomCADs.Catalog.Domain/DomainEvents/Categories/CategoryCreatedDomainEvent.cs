using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.DomainEvents.Categories;

public record CategoryCreatedDomainEvent(
    Category Category
) : DomainEvent;
