using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Catalog.Domain.Categories.DomainEvents;

public record CategoryDeletedDomainEvent(
    CategoryId Id
) : BaseDomainEvent;
