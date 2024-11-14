using CustomCADs.Catalog.Application.Products.Commands.SetCoords;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;

namespace CustomCADs.Catalog.Endpoints.Products.PatchProduct;

using static ApiMessages;

public class PatchProductEndpoint(IRequestSender sender)
    : Endpoint<PatchCadRequest>
{
    public override void Configure()
    {
        Patch("{id}");
        Group<ProductsGroup>();
        Options(o => o.Accepts<PatchCadRequest>("application/json"));
    }

    public override async Task HandleAsync(PatchCadRequest req, CancellationToken ct)
    {
        IsProductCreatorQuery query = new(req.Id, User.GetAccountId());
        bool userIsCreator = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        if (userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        bool IsType(string type) => req.Type.Equals(type, StringComparison.OrdinalIgnoreCase);
        SetProductCoordsCommand command = new(req.Id, User.GetAccountId());

        if (IsType("camera"))
            command = command with { CamCoordinates = req.Coordinates.ToValueObject() };
        else if (IsType("pan"))
            command = command with { PanCoordinates = req.Coordinates.ToValueObject() };
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
