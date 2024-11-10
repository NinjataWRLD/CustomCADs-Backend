using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.Categories.DomainEvents;

public record CategoryCreatedDomainEvent(
    Category Category
) : DomainEvent;
