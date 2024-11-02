using CustomCADs.Catalog.Application.Products.Commands.Delete;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Shared.Core;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Catalog.Endpoints.Products.DeleteProduct;

using static Helpers.ApiMessages;

public class DeleteProductEndpoint(IMessageBus bus) : Endpoint<DeleteProductRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        IsProductCreatorQuery isCreatorQuery = new(req.Id, User.GetId());
        var userIsCreator = await bus.InvokeAsync<bool>(isCreatorQuery, ct).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductByIdQuery getProductQuery = new(req.Id);
        var model = await bus.InvokeAsync<GetProductByIdDto>(getProductQuery, ct).ConfigureAwait(false);

        // Delete image
        // Delete cad

        DeleteProductCommand command = new(req.Id);
        await bus.InvokeAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
