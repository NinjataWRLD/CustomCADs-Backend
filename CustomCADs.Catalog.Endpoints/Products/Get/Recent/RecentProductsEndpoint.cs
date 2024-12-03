using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Get.Recent;

public sealed class RecentProductsEndpoint(IRequestSender sender)
    : Endpoint<RecentProductsRequest, RecentProductsResponse[]>
{
    public override void Configure()
    {
        Get("recent");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("03. Recent")
            .WithDescription("See your most recent Products")
        );
    }

    public override async Task HandleAsync(RecentProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CreatorId: User.GetAccountId(),
            Sorting: new(ProductSortingType.UploadDate, SortingDirection.Descending),
            Limit: req.Limit
        );
        Result<GetAllProductsDto> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        RecentProductsResponse[] response = [.. result.Items.Select(p => p.ToRecentProductsResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
