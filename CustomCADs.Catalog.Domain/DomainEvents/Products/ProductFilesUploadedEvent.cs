using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.DomainEvents.Products;

public record ProductFilesUploadedEvent(Guid Id, string ImagePath, string CadPath) : IEvent;
