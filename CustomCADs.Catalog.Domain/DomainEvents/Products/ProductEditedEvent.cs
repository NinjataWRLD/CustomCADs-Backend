using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Dtos;

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
) : DomainEvent;