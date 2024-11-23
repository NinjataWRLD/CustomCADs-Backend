using CustomCADs.Inventory.Application.Products;
using CustomCADs.Inventory.Application.Products.Commands.SetCoords;
using CustomCADs.Inventory.Application.Products.Queries.IsCreator;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Endpoints.Products.Patch;

using static ApiMessages;

public class PatchProductCadEndpoint(IRequestSender sender)
    : Endpoint<PatchProductCadRequest>
{
    public override void Configure()
    {
        Patch("{id}");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("5. I want to set the Coordinates of my Product's Cad"));
    }

    public override async Task HandleAsync(PatchProductCadRequest req, CancellationToken ct)
    {
        ProductId id = new(req.Id);
        IsProductCreatorQuery query = new(id, User.GetAccountId());
        bool userIsCreator = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        if (userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        bool IsType(string type) => req.Type.Equals(type, StringComparison.OrdinalIgnoreCase);
        SetProductCoordsCommand command = new(id, User.GetAccountId());

        if (IsType("camera"))
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
