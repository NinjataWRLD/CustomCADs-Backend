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

        if (product.CadId is null)
        {
            throw ProductValidationException.CadNotNull(product.Id);
        }

        CadCoordsUpdateRequestedIntegrationEvent cadCoordsUpdateRequestedie = new(
            product.CadId.Value,
            product.CreatorId,
            req.CamCoordinates is null ? null : new(req.CamCoordinates),
            req.PanCoordinates is null ? null : new(req.PanCoordinates)
        );
        await raiser.RaiseAsync(cadCoordsUpdateRequestedie).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
