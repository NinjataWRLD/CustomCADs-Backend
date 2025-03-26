using CustomCADs.Catalog.Application.Common.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Shared.GetAll;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Creator.Get.Recent;

public sealed class RecentProductsEndpoint(IRequestSender sender)
    : Endpoint<RecentProductsRequest, RecentProductsResponse[]>
{
    public override void Configure()
    {
        Get("recent");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("03. Recent")
            .WithDescription("See your most recent Products")
        );
    }

    public override async Task HandleAsync(RecentProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CreatorId: User.GetAccountId(),
            Sorting: new(
                ProductCreatorSortingType.UploadDate.ToBase(),
                SortingDirection.Descending
            ),
            Pagination: new(Limit: req.Limit)
        );
        Result<GetAllProductsDto> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        RecentProductsResponse[] response = [.. result.Items.Select(p => p.ToRecentResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
