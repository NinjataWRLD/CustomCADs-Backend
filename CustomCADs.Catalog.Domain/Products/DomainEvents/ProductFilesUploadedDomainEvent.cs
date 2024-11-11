using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductFilesUploadedDomainEvent(
    ProductId Id,
    string ImagePath,
    string CadPath
) : BaseDomainEvent;
