using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Categories.Domain.Categories.DomainEvents;

public record CategoryEditedDomainEvent(
    CategoryId Id,
    Category Category
) : BaseDomainEvent;