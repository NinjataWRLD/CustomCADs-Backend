using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Catalog.Domain.Categories.DomainEvents;

public record CategoryCreatedDomainEvent(
    Category Category
) : BaseDomainEvent;
