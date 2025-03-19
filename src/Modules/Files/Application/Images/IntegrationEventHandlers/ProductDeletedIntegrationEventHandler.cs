using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.IntegrationEvents.Files;

namespace CustomCADs.Files.Application.Images.IntegrationEventHandlers;

public class ProductDeletedIntegrationEventHandler(IImageReads reads, IWrites<Image> writes, IUnitOfWork uow, IStorageService storage)
{
    public async Task Handle(ProductDeletedIntegrationEvent ie)
    {
        Image image = await reads.SingleByIdAsync(ie.ImageId, track: true).ConfigureAwait(false)
            ?? throw ImageNotFoundException.ById(ie.ImageId);

        await storage.DeleteFileAsync(image.Key).ConfigureAwait(false);

        writes.Remove(image);
        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
