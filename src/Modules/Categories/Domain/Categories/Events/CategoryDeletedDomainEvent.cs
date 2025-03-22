using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Categories.Domain.Categories.Events;

public record CategoryDeletedDomainEvent(
    CategoryId Id
) : BaseDomainEvent;
