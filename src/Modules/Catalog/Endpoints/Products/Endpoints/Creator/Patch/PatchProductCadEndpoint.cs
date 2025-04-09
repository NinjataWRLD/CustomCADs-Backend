using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetCoords;
using System.ComponentModel;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Patch;

public sealed class PatchProductCadEndpoint(IRequestSender sender)
    : Endpoint<PatchProductCadRequest>
{
    public override void Configure()
    {
        Patch("");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("Set Cad Coordinates")
            .WithDescription("Set the Coordinates of your Product's Cad")
        );
    }

    public override async Task HandleAsync(PatchProductCadRequest req, CancellationToken ct)
    {
        await sender.SendCommandAsync(
            req.Type switch
            {
                CoordinateType.Cam => new SetProductCoordsCommand(
                    Id: ProductId.New(req.Id),
                    CreatorId: User.GetAccountId(),
                    CamCoordinates: req.Coordinates
                ),
                CoordinateType.Pan => new SetProductCoordsCommand(
                    Id: ProductId.New(req.Id),
                    CreatorId: User.GetAccountId(),
                    PanCoordinates: req.Coordinates
                ),
                _ => throw new InvalidEnumArgumentException("Coordinate Type must be Cam or Pan"),
            },
            ct
        ).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
