using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Entities;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;


public class SetProductCoordsHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<SetProductCoordsCommand>
{
    public async Task Handle(SetProductCoordsCommand req, CancellationToken ct = default)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        product.SetCoords(
            camCoords: req.CamCoordinates ?? product.Cad.CamCoordinates, 
            panCoords: req.PanCoordinates ?? product.Cad.PanCoordinates
        );
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
