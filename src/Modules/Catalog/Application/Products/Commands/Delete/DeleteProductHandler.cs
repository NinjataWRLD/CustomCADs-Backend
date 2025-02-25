using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.IntegrationEvents.Files;

namespace CustomCADs.Catalog.Application.Products.Commands.Delete;

public sealed class DeleteProductHandler(IProductReads reads, IWrites<Product> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductAuthorizationException.ByProductId(req.Id);
        }

        writes.Remove(product);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseIntegrationEventAsync(new ProductDeletedIntegrationEvent(
            Id: product.Id,
            ImageId: product.ImageId,
            CadId: product.CadId
        )).ConfigureAwait(false);
    }
}
