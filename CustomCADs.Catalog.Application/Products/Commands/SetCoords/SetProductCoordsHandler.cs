﻿using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Catalog.Domain.Shared;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;

public class SetProductCoordsHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<SetProductCoordsCommand>
{
    public async Task Handle(SetProductCoordsCommand req, CancellationToken ct = default)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        Coordinates camCoords = req.CamCoordinates ?? product.Cad.CamCoordinates;
        Coordinates panCoords = req.PanCoordinates ?? product.Cad.PanCoordinates;

        product.Cad = product.Cad with
        {
            CamCoordinates = camCoords,
            PanCoordinates = panCoords,
        };

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
