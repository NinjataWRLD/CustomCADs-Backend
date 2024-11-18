using CustomCADs.Catalog.Application.Products.Queries.Count;
using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Count;

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
        int uncheckedProductsCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Validated };
        int validatedProductsCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Reported };
        int reportedProductsCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Removed };
        int bannedProductsCount = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        CountProductsResponse response = new(uncheckedProductsCount, validatedProductsCount, reportedProductsCount, bannedProductsCount);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
