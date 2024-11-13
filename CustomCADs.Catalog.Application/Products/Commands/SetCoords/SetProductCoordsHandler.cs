using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;

using static Constants.Cads.Coordinates;

public class SetProductCoordsHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<SetProductCoordsCommand>
{
    public async Task Handle(SetProductCoordsCommand req, CancellationToken ct = default)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        Coordinates camCoords = req.CamCoordinates ?? product.Cad.CamCoordinates;
        Coordinates panCoords = req.PanCoordinates ?? product.Cad.PanCoordinates;

        static bool AreCoordsValid(params int[] coords)
            => coords.All(c => c >= CoordMin && c < CoordMax);

        if (!AreCoordsValid(camCoords.X, camCoords.X, camCoords.Z))
        {
            throw ProductValidationException.Range("CamCoordinates", CoordMin, CoordMax);
        }

        if (!AreCoordsValid(panCoords.X, panCoords.Y, panCoords.Z))
        {
            throw ProductValidationException.Range("PanCoordinates", CoordMin, CoordMax);
        }

        product.Cad = product.Cad with
        {
            CamCoordinates = camCoords,
            PanCoordinates = panCoords,
        };

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
