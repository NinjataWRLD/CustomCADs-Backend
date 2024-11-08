﻿using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.DomainEvents.Products;
using CustomCADs.Shared.Application.Storage;
using MediatR;

namespace CustomCADs.Catalog.Application.EventHandlers;

public class ProductDeletedEventHandler(IMediator mediator, IStorageService service)
{
    public async Task Handle(ProductDeletedEvent pdEvent)
    {
        GetProductByIdQuery query = new(pdEvent.Id);
        GetProductByIdDto dto = await mediator.Send(query).ConfigureAwait(false);

        Task imageTask = service.DeleteFileAsync(dto.ImagePath),
            cadTask = service.DeleteFileAsync(dto.Cad.Path);

        await Task.WhenAll(imageTask, cadTask).ConfigureAwait(false);
    }
}

