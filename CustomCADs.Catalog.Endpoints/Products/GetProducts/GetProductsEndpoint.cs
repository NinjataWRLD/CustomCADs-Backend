using CustomCADs.Catalog.Application.Products.Queries.GetAll;

namespace CustomCADs.Catalog.Endpoints.Products.GetProducts;


public class GetProductsEndpoint(IRequestSender sender) 
    : Endpoint<GetProductsRequest, GetProductsResponse>
{
    public override void Configure()
    {
        Get("");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(GetProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CreatorId: User.GetAccountId(),
            Category: req.Category,
            Name: req.Name,
            Sorting: req.Sorting ?? string.Empty,
            Page: req.Page,
            Limit: req.Limit
        );
        GetAllProductsDto result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductsResponse response = new(
            result.Count,
            result.Products.Select(p => new GetProductsDto(p)).ToArray()
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
