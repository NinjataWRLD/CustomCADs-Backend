using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Catalog;

namespace CustomCADs.Catalog.Application.Products.Commands.SetKeys;

public class SetProductKeysHandler(IProductReads reads, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<SetProductKeysCommand>
{
    public async Task Handle(SetProductKeysCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (req.ImageKey is not null)
        {
            product.SetImage(req.ImageKey);
            await uow.SaveChangesAsync(ct).ConfigureAwait(false);
        }

        if (req.CadKey is not null)
        {
            await raiser.RaiseIntegrationEventAsync(new CadKeyUpdateRequestedIntegrationEvent(
                product.CadId,
                req.CadKey
            )).ConfigureAwait(false);
        }
    }
}
