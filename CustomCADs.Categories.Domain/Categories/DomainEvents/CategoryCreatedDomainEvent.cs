using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Categories.Domain.Categories.DomainEvents;

public record CategoryCreatedDomainEvent(
    Category Category
) : BaseDomainEvent;
