using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;

namespace CustomCADs.Catalog.Endpoints.Products.GetProduct;

using static ApiMessages;

public class GetProductEndpoint(IMediator mediator) : Endpoint<GetProductRequest, GetProductResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {
        IsProductCreatorQuery isCreatorQuery = new(req.Id, User.GetAccountId());
        bool userIsCreator = await mediator.Send(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductByIdQuery getProductQuery = new(req.Id);
        GetProductByIdDto product = await mediator.Send(getProductQuery, ct).ConfigureAwait(false);

        GetProductResponse response = new(product);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
