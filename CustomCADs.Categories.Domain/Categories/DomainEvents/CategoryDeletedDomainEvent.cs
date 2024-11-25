using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Categories.Domain.Categories.DomainEvents;

public record CategoryDeletedDomainEvent(
    CategoryId Id
) : BaseDomainEvent;
