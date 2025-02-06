using CustomCADs.Catalog.Application.Products.Commands.SetCoords;

namespace CustomCADs.Catalog.Endpoints.Products.Creator.Patch;

public sealed class PatchProductCadEndpoint(IRequestSender sender)
    : Endpoint<PatchProductCadRequest>
{
    public override void Configure()
    {
        Patch("");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("08. Set Cad Coordinates")
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
