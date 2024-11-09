using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.DomainEvents.Products;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Catalog.Application.EventHandlers;

public class ProductDeletedEventHandler(IRequestSender sender, IStorageService service)
{
    public async Task Handle(ProductDeletedEvent pdEvent)
    {
        GetProductByIdQuery query = new(pdEvent.Id);
        GetProductByIdDto dto = await sender.SendQueryAsync(query).ConfigureAwait(false);

        Task imageTask = service.DeleteFileAsync(dto.ImagePath),
            cadTask = service.DeleteFileAsync(dto.Cad.Path);

        await Task.WhenAll(imageTask, cadTask).ConfigureAwait(false);
    }
}

