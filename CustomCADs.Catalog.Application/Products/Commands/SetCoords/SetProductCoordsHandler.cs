using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Commands.Cads;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;

public class SetProductCoordsHandler(IProductReads reads, IRequestSender sender)
    : ICommandHandler<SetProductCoordsCommand>
{
    public async Task Handle(SetProductCoordsCommand req, CancellationToken ct = default)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        SetCadCoordsCommand command = new(
            Id: product.CadId,
            CamCoordinates: req.CamCoordinates?.ToCoordinatesDto(),
            PanCoordinates: req.PanCoordinates?.ToCoordinatesDto()
        );
        await sender.SendCommandAsync(command, ct);
    }
}
