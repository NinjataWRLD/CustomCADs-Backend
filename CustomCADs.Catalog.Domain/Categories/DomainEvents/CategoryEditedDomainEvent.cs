using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Domain.Categories.DomainEvents;

public record CategoryEditedDomainEvent(
    CategoryId Id,
    Category Category
) : BaseDomainEvent;