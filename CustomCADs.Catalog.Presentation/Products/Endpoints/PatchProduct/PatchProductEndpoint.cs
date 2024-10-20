using CustomCADs.Catalog.Application.Products.Commands.Edit;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Catalog.Presentation.Extensions;
using FastEndpoints;
using Mapster;
using MediatR;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.PatchProduct;

using static Helpers.ApiMessages;

public class PatchProductEndpoint(IMediator mediator) : Endpoint<PatchCadRequest>
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
        bool userIsCreator = await mediator.Send(isCreatorQuery, ct).ConfigureAwait(false);

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
        GetProductByIdDto product = await mediator.Send(getCadQuery, ct).ConfigureAwait(false);

        var coords = req.Coordinates.Adapt<Coordinates>();
        switch (req.Type.ToLower())
        {
            case "camera":
                product.Cad = product.Cad with { CamCoordinates = coords };
                break;

            case "pan":
                product.Cad = product.Cad with { PanCoordinates = coords };
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

        EditProductDto dto = new()
        {
            Name = product.Name,
            Description = product.Description,
            Cost = product.Cost,
            CategoryId = product.Category.Id,
        };
        EditProductCommand command = new(req.Id, dto);
        await mediator.Send(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
