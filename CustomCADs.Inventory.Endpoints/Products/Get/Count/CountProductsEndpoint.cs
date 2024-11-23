using CustomCADs.Inventory.Application.Products.Queries.Count;
using CustomCADs.Inventory.Domain.Products.Enums;

namespace CustomCADs.Inventory.Endpoints.Products.Get.Count;

public class CountProductsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<CountProductsResponse>
{
    public override void Configure()
    {
        Get("count");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("4. I want to see the count of my Products by status"));
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
