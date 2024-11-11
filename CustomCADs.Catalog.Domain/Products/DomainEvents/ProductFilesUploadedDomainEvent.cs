using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductFilesUploadedDomainEvent(
    ProductId Id,
    string ImagePath,
    string CadPath
) : BaseDomainEvent;
