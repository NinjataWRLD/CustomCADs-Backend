using CustomCADs.Catalog.Domain.DomainEvents.Products;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Application.EventHandlers;

public class ProductCreatedEventHandler(IStorageService service, IEventRaiser raiser)
{
    public async Task Handle(ProductCreatedEvent pcEvent)
    {
        using MemoryStream imageStream = new(pcEvent.Image.Bytes);
        using MemoryStream cadStream = new(pcEvent.Cad.Bytes);

        string imagePath = await service.UploadFileAsync(
            "images",
            imageStream,
            pcEvent.Id,
            pcEvent.Name,
            pcEvent.Image.ContentType,
            pcEvent.Image.FileName
        ).ConfigureAwait(false);

        string cadPath = await service.UploadFileAsync(
            "cads",
            cadStream,
            pcEvent.Id,
            pcEvent.Name,
            pcEvent.Cad.ContentType,
            pcEvent.Cad.FileName
        ).ConfigureAwait(false);

        ProductFilesUploadedEvent pfuEvent = new(pcEvent.Id, imagePath, cadPath);
        await raiser.PublishAsync(pfuEvent).ConfigureAwait(false);
    }
}
