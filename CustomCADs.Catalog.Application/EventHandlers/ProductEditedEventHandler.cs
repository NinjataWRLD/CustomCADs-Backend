using CustomCADs.Catalog.Application.Products.Commands.SetPaths;
using CustomCADs.Shared.Core.Events.Products;
using CustomCADs.Shared.Core.Storage;
using MediatR;

namespace CustomCADs.Catalog.Application.EventHandlers;

public class ProductEditedEventHandler(IStorageService service, IMediator mediator)
{
    public async Task Handle(ProductEditedEvent peEvent)
    {
        if (peEvent.Image == null)
        {
            return;
        }
        await service.DeleteFileAsync(peEvent.OldImagePath).ConfigureAwait(false);

        using MemoryStream stream = new(peEvent.Image.Bytes);
        string path = await service.UploadFileAsync(        
            "images",
            stream,
            peEvent.Image.ContentType,
            peEvent.Image.FileName
        ).ConfigureAwait(false);

        SetProductPathsCommand command = new(peEvent.Id, ImagePath: path);
        await mediator.Send(command).ConfigureAwait(false);
    }
}
