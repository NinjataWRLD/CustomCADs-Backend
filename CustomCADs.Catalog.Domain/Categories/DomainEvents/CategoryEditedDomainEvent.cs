using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.Categories.DomainEvents;

public record CategoryEditedDomainEvent(
    int Id,
    Category Category
) : DomainEvent;