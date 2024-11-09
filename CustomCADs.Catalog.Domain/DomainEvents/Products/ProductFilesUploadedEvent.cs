using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.DomainEvents.Products;

public record ProductFilesUploadedEvent(Guid Id, string ImagePath, string CadPath) : DomainEvent;
