namespace CustomCADs.Shared.Core.Events.Products;

public record ProductFilesUploadedEvent(Guid Id, string ImagePath, string CadPath) : IEvent;
