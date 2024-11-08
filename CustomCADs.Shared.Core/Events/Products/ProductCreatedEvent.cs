using CustomCADs.Shared.Core.Dtos;

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
) : IEvent;
