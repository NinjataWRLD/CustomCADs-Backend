﻿using CustomCADs.Catalog.Application.Products.Commands.SetPaths;
using CustomCADs.Catalog.Domain.DomainEvents.Products;
using CustomCADs.Shared.Application.Storage;
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

        using MemoryStream stream = new(peEvent.Image.Bytes);
        string path = await service.UploadFileAsync(
            "images",
            stream,
            peEvent.Id,
            peEvent.Name,
            peEvent.Image.ContentType,
            peEvent.Image.FileName
        ).ConfigureAwait(false);

        await service.DeleteFileAsync(peEvent.OldImagePath).ConfigureAwait(false);

        SetProductPathsCommand command = new(peEvent.Id, ImagePath: path);
        await mediator.Send(command).ConfigureAwait(false);
    }
}