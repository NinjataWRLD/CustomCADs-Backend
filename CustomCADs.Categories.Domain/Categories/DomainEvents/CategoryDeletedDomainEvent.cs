using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Categories.Domain.Categories.DomainEvents;

public record CategoryDeletedDomainEvent(
    CategoryId Id
) : BaseDomainEvent;
