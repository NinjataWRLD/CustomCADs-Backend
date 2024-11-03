using CustomCADs.Catalog.Application.Products.Commands.Edit;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Shared.Core;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CustomCADs.Catalog.Endpoints.Products.PutProduct;

using static Helpers.ApiMessages;

public class PutProductEndpoint(IMediator mediator) : Endpoint<PutProductRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<ProductsGroup>();
        Options(o => o.Accepts<PutProductRequest>("multipart/form-data"));
    }

    public override async Task HandleAsync(PutProductRequest req, CancellationToken ct)
    {
        IsProductCreatorQuery isCreatorQuery = new(req.Id, User.GetId());
        bool userIsCreator = await mediator.Send(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductByIdQuery query = new(req.Id);
        GetProductByIdDto product = await mediator.Send(query, ct).ConfigureAwait(false);

        if (req.Image is not null)
        {
            // Upload new image
            // Save its path
        }

        EditProductDto dto = new(
            Name: req.Name,
            Description: req.Description,
            CategoryId: req.CategoryId,
            Cost: req.Cost
        );
        EditProductCommand editCommand = new(req.Id, dto);
        await mediator.Send(editCommand, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
