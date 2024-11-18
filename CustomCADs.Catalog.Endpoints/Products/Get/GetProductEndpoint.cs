using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;

namespace CustomCADs.Catalog.Endpoints.Products.Get;

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
        ProductId id = new(req.Id);
        IsProductCreatorQuery isCreatorQuery = new(id, User.GetAccountId());
        bool userIsCreator = await sender.SendQueryAsync(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductByIdQuery getProductQuery = new(id);
        GetProductByIdDto product = await sender.SendQueryAsync(getProductQuery, ct).ConfigureAwait(false);

        GetProductResponse response = product.ToGetProductResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
