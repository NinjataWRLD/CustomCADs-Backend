using CustomCADs.Inventory.Application.Common.Exceptions;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Inventory.Application.Products.Commands.SetCoords;

public class SetProductCoordsHandler(IProductReads reads, IRequestSender sender)
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
        await sender.SendCommandAsync(command, ct);
    }
}
