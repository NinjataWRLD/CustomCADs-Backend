using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;
using CustomCADs.Shared.Core.Dtos;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductEditedDomainEvent(
    ProductId Id,
    string OldName,
    string Name,
    string OldDescription,
    string Description,
    CategoryId OldCategoryId,
    CategoryId CategoryId,
    Money OldPrice,
    Money Price,
    string OldImagePath,
    FileDto? Image = default
) : BaseDomainEvent;