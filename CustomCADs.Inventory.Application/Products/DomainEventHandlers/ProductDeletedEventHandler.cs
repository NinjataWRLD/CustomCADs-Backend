using CustomCADs.Inventory.Domain.Products.DomainEvents;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Inventory.Application.Products.DomainEventHandlers;

public class ProductDeletedEventHandler(IStorageService storage)
{
    public async Task Handle(ProductDeletedDomainEvent de)
    {
        Task imageTask = storage.DeleteFileAsync(de.ImageKey);
        Task cadTask = storage.DeleteFileAsync(de.CadKey);

        await Task.WhenAll(imageTask, cadTask).ConfigureAwait(false);
    }
}

