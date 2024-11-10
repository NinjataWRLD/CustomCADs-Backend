using CustomCADs.Shared.Core.Dtos;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductCreatedDomainEvent(
    Guid Id,
    string Name,
    string Description,
    int CategoryId,
    decimal Cost,
    Guid CreatorId,
    string Status,
    FileDto Image,
    FileDto Cad
) : DomainEvent;
