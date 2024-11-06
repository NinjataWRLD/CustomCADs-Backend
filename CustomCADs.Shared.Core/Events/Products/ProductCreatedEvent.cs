using CustomCADs.Shared.Core.Storage.Dtos;

namespace CustomCADs.Shared.Core.Events.Products;

public record ProductCreatedEvent(
    Guid Id,
    string Name,
    string Description,
    int CategoryId,
    decimal Cost,
    Guid CreatorId,
    string Status,
    FileDto Image,
    FileDto Cad
);
