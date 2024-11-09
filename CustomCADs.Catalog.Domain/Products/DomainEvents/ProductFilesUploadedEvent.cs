using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductFilesUploadedEvent(
    Guid Id,
    string ImagePath,
    string CadPath
) : DomainEvent;
