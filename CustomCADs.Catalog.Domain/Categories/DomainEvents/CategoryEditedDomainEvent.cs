using CustomCADs.Catalog.Domain.Categories.Entities;
using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Catalog.Domain.Categories.DomainEvents;

public record CategoryEditedDomainEvent(
    CategoryId Id,
    Category Category
) : BaseDomainEvent;