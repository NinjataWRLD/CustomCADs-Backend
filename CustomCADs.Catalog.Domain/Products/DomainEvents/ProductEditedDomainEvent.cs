using CustomCADs.Shared.Core.Dtos;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductEditedDomainEvent(
    Guid Id,
    string OldName,
    string Name,
    string OldDescription,
    string Description,
    int OldCategoryId,
    int CategoryId,
    decimal OldCost,
    decimal Cost,
    string OldImagePath,
    FileDto? Image = default
) : DomainEvent;