using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.DomainEvents;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Commands.Delete;

public sealed class DeleteProductHandler(IProductReads productReads, IWrites<Product> productWrites, IUnitOfWork uow, IRequestSender sender, IEventRaiser raiser)
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

        GetImageByIdQuery imageQuery = new(product.ImageId);
        var (_, ImageKey, _) = await sender.SendQueryAsync(imageQuery, ct).ConfigureAwait(false);

        GetCadByIdQuery cadQuery = new(product.CadId);
        var (CadKey, _, _, _) = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new ProductDeletedDomainEvent(
            Id: product.Id,
            ImageKey: ImageKey,
            CadKey: CadKey
        )).ConfigureAwait(false);
    }
}
