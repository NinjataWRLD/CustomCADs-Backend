using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.Products.DomainEvents;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Catalog.Application.Products.DomainEventHandlers;

public class ProductDeletedEventHandler(IRequestSender sender, IStorageService service)
{
    public async Task Handle(ProductDeletedDomainEvent de)
    {
        GetProductByIdQuery query = new(de.Id);
        GetProductByIdDto dto = await sender.SendQueryAsync(query).ConfigureAwait(false);

        Task imageTask = service.DeleteFileAsync(dto.Image.Key);
        Task cadTask = service.DeleteFileAsync(dto.Cad.Key);

        await Task.WhenAll(imageTask, cadTask).ConfigureAwait(false);
    }
}

