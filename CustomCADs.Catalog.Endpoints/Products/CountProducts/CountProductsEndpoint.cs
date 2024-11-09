using CustomCADs.Catalog.Application.Products.Queries.Count;
using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.CountProducts;

public class CountProductsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<CountProductsResponse>
{
    public override void Configure()
    {
        Get("count");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        ProductsCountQuery query = new(User.GetAccountId(), ProductStatus.Unchecked);
        var uncheckedProductsCounts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Validated };
        var validatedProductsCounts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Reported };
        var reportedProductsCounts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Removed };
        var bannedProductsCounts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        CountProductsResponse response = new(uncheckedProductsCounts, validatedProductsCounts, reportedProductsCounts, bannedProductsCounts);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
