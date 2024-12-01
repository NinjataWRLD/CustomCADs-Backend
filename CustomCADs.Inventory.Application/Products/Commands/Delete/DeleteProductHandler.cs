using CustomCADs.Inventory.Application.Common.Exceptions;
using CustomCADs.Inventory.Domain.Common;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.DomainEvents;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Inventory.Application.Products.Commands.Delete;

public class DeleteProductHandler(IProductReads productReads, IWrites<Product> productWrites, IUnitOfWork uow, IRequestSender sender, IEventRaiser raiser)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand req, CancellationToken ct)
    {
        Product product = await productReads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductAuthorizationException.ByProductId(req.Id);
        }

        productWrites.Remove(product);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        GetCadByIdQuery query = new(product.CadId);
        var (Key, _, _, _) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new ProductDeletedDomainEvent(
            Id: product.Id,
            ImageKey: product.Image.Key,
            CadKey: Key
        )).ConfigureAwait(false);
    }
}
