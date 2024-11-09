using CustomCADs.Catalog.Application.Products.Commands.SetPaths;
using CustomCADs.Catalog.Domain.DomainEvents.Products;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Catalog.Application.Products.DomainEventHandlers;

public class ProductEditedEventHandler(IStorageService service, IRequestSender sender)
{
    public async Task Handle(ProductEditedDomainEvent de)
    {
        if (de.Image == null)
        {
            return;
        }

        using MemoryStream stream = new(de.Image.Bytes);
        string path = await service.UploadFileAsync(
            "images",
            stream,
            de.Id,
            de.Name,
            de.Image.ContentType,
            de.Image.FileName
        ).ConfigureAwait(false);

        await service.DeleteFileAsync(de.OldImagePath).ConfigureAwait(false);

        SetProductPathsCommand command = new(de.Id, ImagePath: path);
        await sender.SendCommandAsync(command).ConfigureAwait(false);
    }
}
