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

        CadCoordsUpdateRequestedIntegrationEvent cadCoordsUpdateRequestedie = new(
            Id: product.CadId ?? throw ProductValidationException.CadNotNull(product.Id),
            CamCoordinates: req.CamCoordinates?.ToCoordinatesDto(),
            PanCoordinates: req.PanCoordinates?.ToCoordinatesDto(),            
            CreatorId: product.CreatorId
        );
        await raiser.RaiseAsync(cadCoordsUpdateRequestedie).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
