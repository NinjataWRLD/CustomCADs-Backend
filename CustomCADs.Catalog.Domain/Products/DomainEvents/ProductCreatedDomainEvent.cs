using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;
using CustomCADs.Shared.Core.Dtos;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductCreatedDomainEvent(
    ProductId Id,
    string Name,
    string Description,
    CategoryId CategoryId,
    Money Price,
    UserId CreatorId,
    string Status,
    FileDto Image,
    FileDto Cad
) : BaseDomainEvent;
