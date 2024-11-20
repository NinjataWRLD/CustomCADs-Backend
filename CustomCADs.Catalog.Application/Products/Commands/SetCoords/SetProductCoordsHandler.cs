using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products.Entities;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Catalog;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;

public class SetProductCoordsHandler(IProductReads reads, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<SetProductCoordsCommand>
{
    public async Task Handle(SetProductCoordsCommand req, CancellationToken ct = default)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        await raiser.RaiseIntegrationEventAsync(new CadCoordsUpdateRequestedIntegrationEvent(
            Id: product.CadId,
            CamCoordinates: req.CamCoordinates?.ToCoordinatesDto(),
            PanCoordinates: req.PanCoordinates?.ToCoordinatesDto(),
            CreatorId: product.CreatorId
        )).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
