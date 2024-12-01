using CustomCADs.Inventory.Application.Products;
using CustomCADs.Inventory.Application.Products.Commands.SetCoords;

namespace CustomCADs.Inventory.Endpoints.Products.Patch;

using static ApiMessages;

public class PatchProductCadEndpoint(IRequestSender sender)
    : Endpoint<PatchProductCadRequest>
{
    public override void Configure()
    {
        Patch("{id}");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("08. Set Cad Coordinates")
            .WithDescription("Set the Coordinates of your Product's Cad by specifying the Product's Id and the Cad's Coordinates Type and Values")
        );
    }

    public override async Task HandleAsync(PatchProductCadRequest req, CancellationToken ct)
    {
        SetProductCoordsCommand command = new(
            Id: new(req.Id),
            CreatorId: User.GetAccountId()
        );
        bool IsType(string type) => req.Type.Equals(type, StringComparison.OrdinalIgnoreCase);

        if (IsType("camera") || IsType("cam"))
            command = command with { CamCoordinates = req.Coordinates.ToCoordinates() };
        else if (IsType("pan"))
            command = command with { PanCoordinates = req.Coordinates.ToCoordinates() };
        else
        {
            ValidationFailures.Add(new("Type", InvalidCoordValue, req.Type));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
