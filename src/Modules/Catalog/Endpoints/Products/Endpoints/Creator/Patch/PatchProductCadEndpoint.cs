using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetCoords;

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
        SetProductCoordsCommand command = new(
            Id: ProductId.New(req.Id),
            CreatorId: User.GetAccountId()
        );

        command = req.Type switch
        {
            CoordinateType.Cam => command with { CamCoordinates = req.Coordinates },
            CoordinateType.Pan => command with { PanCoordinates = req.Coordinates },
            _ => command,
        };
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
