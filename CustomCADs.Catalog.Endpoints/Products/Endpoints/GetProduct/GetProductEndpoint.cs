using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Shared.Presentation;
using FastEndpoints;
using Mapster;
using Wolverine;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.GetProduct;

using static CustomCADs.Catalog.Endpoints.Products.Helpers.ApiMessages;

public class GetProductEndpoint(IMessageBus bus) : Endpoint<GetProductRequest, GetProductResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {
        IsProductCreatorQuery isCreatorQuery = new(req.Id, User.GetId());
        var userIsCreator = await bus.InvokeAsync<bool>(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new()
            {
                ErrorMessage = ForbiddenAccess,
            });
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductByIdQuery getProductQuery = new(req.Id);
        var product = await bus.InvokeAsync<GetProductByIdDto>(getProductQuery, ct).ConfigureAwait(false);

        var response = product.Adapt<GetProductResponse>();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
