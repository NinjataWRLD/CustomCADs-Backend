using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Dtos;

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
