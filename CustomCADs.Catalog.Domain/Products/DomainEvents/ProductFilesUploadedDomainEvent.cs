using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductFilesUploadedDomainEvent(
    ProductId Id,
    string ImagePath,
    string CadPath
) : BaseDomainEvent;
