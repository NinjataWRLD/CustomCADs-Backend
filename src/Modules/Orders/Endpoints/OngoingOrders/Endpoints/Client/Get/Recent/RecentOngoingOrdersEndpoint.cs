using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetAll;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.Recent;

public sealed class RecentOngoingOrdersEndpoint(IRequestSender sender)
    : Endpoint<RecentOngoingOrdersRequest, RecentOngoingOrdersResponse[]>
{
    public override void Configure()
    {
        Get("recent");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("04. Recent")
            .WithDescription("See your most recent Orders")
        );
    }

    public override async Task HandleAsync(RecentOngoingOrdersRequest req, CancellationToken ct)
    {
        GetAllOngoingOrdersQuery query = new(
            BuyerId: User.GetAccountId(),
            Sorting: new(OngoingOrderSortingType.OrderDate, SortingDirection.Descending),
            Pagination: new(Limit: req.Limit)
        );
        Result<GetAllOngoingOrdersDto> orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        RecentOngoingOrdersResponse[] response =
            [.. orders.Items.Select(o => o.ToRecentResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
