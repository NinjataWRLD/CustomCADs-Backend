using CustomCADs.Shared.Core.Dtos;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.DomainEvents.Products;

public record ProductEditedEvent(
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
) : IEvent;