using CustomCADs.Inventory.Application.Products.Queries.Count;

namespace CustomCADs.Inventory.Endpoints.Products.Get.Count;

public class CountProductsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<CountProductsResponse>
{
    public override void Configure()
    {
        Get("count");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("04. Count")
            .WithDescription("4. See the Count of your Products grouped by their Status")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        ProductsCountQuery query = new(User.GetAccountId());
        ProductsCountDto counts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        CountProductsResponse response = new(
            Unchecked: counts.Unchecked,
            Validated: counts.Validated,
            Reported: counts.Reported,
            Banned: counts.Banned
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
