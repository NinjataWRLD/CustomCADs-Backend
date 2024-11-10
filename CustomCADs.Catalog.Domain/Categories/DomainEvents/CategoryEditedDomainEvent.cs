using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.Categories.DomainEvents;

public record CategoryEditedDomainEvent(
    int Id,
    Category Category
) : DomainEvent;