using CustomCADs.Shared.Core.Storage.Dtos;

namespace CustomCADs.Shared.Core.Events.Products;

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
);