using CustomCADs.Catalog.Application.Products.Commands.SetCoords;

namespace CustomCADs.Catalog.Endpoints.Products.Creator.Patch;

public sealed class PatchProductCadEndpoint(IRequestSender sender)
    : Endpoint<PatchProductCadRequest>
{
    public override void Configure()
    {
        Patch("");
        Group<ProductsGroup>();
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

        switch (req.Type)
        {
            case CoordinateType.Cam:
                command = command with { CamCoordinates = req.Coordinates };
                break;

            case CoordinateType.Pan:
                command = command with { PanCoordinates = req.Coordinates };
                break;

            default:
                string types = string.Join(", ", Enum.GetNames<CoordinateType>());
                ValidationFailures.Add(new(
                    propertyName: nameof(req.Type),
                    errorMessage: $"Type property must be one of: [{types}]",
                    attemptedValue: req.Type
                ));
                await SendErrorsAsync().ConfigureAwait(false);
                return;
        }
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
