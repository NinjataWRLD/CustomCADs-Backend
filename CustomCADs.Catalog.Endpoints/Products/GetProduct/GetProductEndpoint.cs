using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;

namespace CustomCADs.Catalog.Endpoints.Products.GetProduct;

using static ApiMessages;

public class GetProductEndpoint(IRequestSender sender) 
    : Endpoint<GetProductRequest, GetProductResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {
        IsProductCreatorQuery isCreatorQuery = new(req.Id, User.GetAccountId());
        bool userIsCreator = await sender.SendQueryAsync(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductByIdQuery getProductQuery = new(req.Id);
        GetProductByIdDto product = await sender.SendQueryAsync(getProductQuery, ct).ConfigureAwait(false);

        GetProductResponse response = new(product);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
