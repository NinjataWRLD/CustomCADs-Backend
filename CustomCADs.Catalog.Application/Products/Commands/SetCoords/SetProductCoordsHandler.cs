using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;

public sealed class SetProductCoordsHandler(IProductReads reads, IRequestSender sender)
    : ICommandHandler<SetProductCoordsCommand>
{
    public async Task Handle(SetProductCoordsCommand req, CancellationToken ct = default)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductAuthorizationException.ByProductId(req.Id);
        }

        SetCadCoordsCommand command = new(
            Id: product.CadId,
            CamCoordinates: req.CamCoordinates,
            PanCoordinates: req.PanCoordinates
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
    }
}
