using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.DomainEvents;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.IntegrationEvents.Catalog;
using CustomCADs.Shared.Queries.Cads;

namespace CustomCADs.Catalog.Application.Products.Commands.Delete;

public class DeleteProductHandler(IProductReads productReads, IWrites<Product> productWrites, IUnitOfWork uow, IRequestSender sender, IEventRaiser raiser)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand req, CancellationToken ct)
    {
        Product product = await productReads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        productWrites.Remove(product);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        GetCadByIdQuery query = new(product.CadId);
        var (Key, _, _, _) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new ProductDeletedDomainEvent(
            Id: product.Id,
            ImageKey: product.Image.Key,
            CadKey: Key
        )).ConfigureAwait(false);

        await raiser.RaiseIntegrationEventAsync(new ProductDeletedIntegrationEvent(
            Id: product.Id
        )).ConfigureAwait(false);
    }
}
