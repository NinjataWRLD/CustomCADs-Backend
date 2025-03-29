using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Files;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Delete;

public sealed class DeleteProductHandler(IProductReads reads, IProductWrites writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw CustomAuthorizationException<Product>.ById(req.Id);
        }

        writes.Remove(product);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseApplicationEventAsync(new ProductDeletedApplicationEvent(
            Id: product.Id,
            ImageId: product.ImageId,
            CadId: product.CadId
        )).ConfigureAwait(false);
    }
}
