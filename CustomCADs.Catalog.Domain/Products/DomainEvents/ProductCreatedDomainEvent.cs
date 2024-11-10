using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Dtos;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductCreatedDomainEvent(
    Guid Id,
    string Name,
    string Description,
    int CategoryId,
    Money Price,
    Guid CreatorId,
    string Status,
    FileDto Image,
    FileDto Cad
) : DomainEvent;
