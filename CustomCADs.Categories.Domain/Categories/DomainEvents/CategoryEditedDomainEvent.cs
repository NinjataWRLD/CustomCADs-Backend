using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Categories.Domain.Categories.DomainEvents;

public record CategoryEditedDomainEvent(
    CategoryId Id,
    Category Category
) : BaseDomainEvent;