using CustomCADs.Catalog.Domain.DomainEvents.Products;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Catalog.Application.Products.DomainEventHandlers;

public class ProductCreatedEventHandler(IStorageService service, IEventRaiser raiser)
{
    public async Task Handle(ProductCreatedDomainEvent de)
    {
        using MemoryStream imageStream = new(de.Image.Bytes);
        using MemoryStream cadStream = new(de.Cad.Bytes);

        string imagePath = await service.UploadFileAsync(
            "images",
            imageStream,
            de.Id,
            de.Name,
            de.Image.ContentType,
            de.Image.FileName
        ).ConfigureAwait(false);

        string cadPath = await service.UploadFileAsync(
            "cads",
            cadStream,
            de.Id,
            de.Name,
            de.Cad.ContentType,
            de.Cad.FileName
        ).ConfigureAwait(false);

        ProductFilesUploadedEvent pfuEvent = new(de.Id, imagePath, cadPath);
        await raiser.RaiseAsync(pfuEvent).ConfigureAwait(false);
    }
}
