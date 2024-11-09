using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.RecentProducts;

public class RecentProductsEndpoint(IRequestSender sender)
    : Endpoint<RecentProductsRequest, IEnumerable<RecentProductsResponse>>
{
    public override void Configure()
    {
        Get("recent");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(RecentProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CreatorId: User.GetAccountId(),
            Sorting: nameof(ProductSorting.Newest),
            Limit: req.Limit
        );
        GetAllProductsDto dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = dto.Products.Select(p => new RecentProductsResponse(p));
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
