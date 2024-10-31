using CustomCADs.Catalog.Application.Products.Commands.Edit;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Shared.Presentation;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Wolverine;

namespace CustomCADs.Catalog.Endpoints.Products.PutProduct;

using static Helpers.ApiMessages;

public class PutProductEndpoint(IMessageBus bus) : Endpoint<PutProductRequest>
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
        var userIsCreator = await bus.InvokeAsync<bool>(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductByIdQuery query = new(req.Id);
        var product = await bus.InvokeAsync<GetProductByIdDto>(query, ct).ConfigureAwait(false);

        if (req.Image != null)
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
        await bus.InvokeAsync(editCommand, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
