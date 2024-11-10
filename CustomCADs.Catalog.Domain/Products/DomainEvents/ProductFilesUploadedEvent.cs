using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductFilesUploadedEvent(
    Guid Id,
    string ImagePath,
    string CadPath
) : BaseDomainEvent;
