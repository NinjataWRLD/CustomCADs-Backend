using CustomCADs.Catalog.Application.Products.Commands.SetCad;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Presentation;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Wolverine;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.PatchProduct;

using static CustomCADs.Catalog.Endpoints.Products.Helpers.ApiMessages;

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
            ValidationFailures.Add(new()
            {
                ErrorMessage = ForbiddenAccess,
            });
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
                ValidationFailures.Add(new()
                {
                    PropertyName = nameof(req.Type),
                    AttemptedValue = req.Type,
                    ErrorMessage = "Type property must be either 'camera' or 'pan'",
                });
                await SendErrorsAsync().ConfigureAwait(false);
                return;
        }

        SetProductCadDto dto = new(product.Cad.Path, product.Cad.CamCoordinates, product.Cad.PanCoordinates);
        SetProductCadCommand command = new(req.Id, dto);
        await bus.InvokeAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
