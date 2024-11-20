using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products.Entities;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Catalog;

namespace CustomCADs.Catalog.Application.Products.Commands.SetPaths;

public class SetProductPathsHandler(IProductReads reads, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<SetProductPathsCommand>
{
    public async Task Handle(SetProductPathsCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        product.SetImagePath(req.ImagePath);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        if (req.CadPath is not null)
        {
            await raiser.RaiseIntegrationEventAsync(new CadPathUpdateRequestedIntegrationEvent(
                product.CadId,
                req.CadPath
            )).ConfigureAwait(false);
        }
    }
}
