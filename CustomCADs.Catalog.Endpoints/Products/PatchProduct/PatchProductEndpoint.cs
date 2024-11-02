using CustomCADs.Catalog.Application.Products.Commands.SetPaths;
using CustomCADs.Catalog.Application.Products.Commands.SetCoords;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Wolverine;

namespace CustomCADs.Catalog.Endpoints.Products.PatchProduct;

using static Helpers.ApiMessages;

public class PatchProductEndpoint(IMessageBus bus) : Endpoint<PatchCadRequest>
{
    public override void Configure()
    {
        Patch("{id}");
        Group<ProductsGroup>();
        Options(o => o.Accepts<PatchCadRequest>("application/json"));
    }

    public override async Task HandleAsync(PatchCadRequest req, CancellationToken ct)
    {
        IsProductCreatorQuery isCreatorQuery = new(req.Id, User.GetId());
        var userIsCreator = await bus.InvokeAsync<bool>(isCreatorQuery, ct).ConfigureAwait(false);

        if (userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductByIdQuery getCadQuery = new(req.Id);
        var product = await bus.InvokeAsync<GetProductByIdDto>(getCadQuery, ct).ConfigureAwait(false);

        Coordinates coords = new(req.Coordinates.X, req.Coordinates.Y, req.Coordinates.Z);
        Cad newCad;
        switch (req.Type.ToLower())
        {
            case "camera":
                newCad = product.Cad with { CamCoordinates = coords };
                break;

            case "pan":
                newCad = product.Cad with { PanCoordinates = coords };
                break;

            default:
                ValidationFailures.Add(new("Type", InvalidCoordValue, req.Type));
                await SendErrorsAsync().ConfigureAwait(false);
                return;
        }

        SetProductCoordsCommand command = new(req.Id, product.Cad.CamCoordinates, product.Cad.PanCoordinates);
        await bus.InvokeAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
