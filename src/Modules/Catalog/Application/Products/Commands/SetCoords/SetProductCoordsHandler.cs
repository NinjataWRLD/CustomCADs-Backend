using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;

public sealed class SetProductCoordsHandler(IProductReads reads, IRequestSender sender)
    : ICommandHandler<SetProductCoordsCommand>
{
    public async Task Handle(SetProductCoordsCommand req, CancellationToken ct = default)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw CustomAuthorizationException<Product>.ById(req.Id);
        }

        SetCadCoordsCommand command = new(
            Id: product.CadId,
            CamCoordinates: req.CamCoordinates,
            PanCoordinates: req.PanCoordinates
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
    }
}
