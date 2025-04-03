using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.Recent;

public sealed class RecentCustomsEndpoint(IRequestSender sender)
    : Endpoint<RecentCustomsRequest, RecentCustomsResponse[]>
{
    public override void Configure()
    {
        Get("recent");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Recent")
            .WithDescription("See your most recent Customs")
        );
    }

    public override async Task HandleAsync(RecentCustomsRequest req, CancellationToken ct)
    {
        GetAllCustomsQuery query = new(
            BuyerId: User.GetAccountId(),
            Sorting: new(CustomSortingType.OrderedAt, SortingDirection.Descending),
            Pagination: new(Limit: req.Limit)
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        RecentCustomsResponse[] response =
            [.. orders.Items.Select(o => o.ToRecentResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
