using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Endpoints.Client.Get.Recent;

public sealed class RecentOrdersEndpoint(IRequestSender sender)
    : Endpoint<RecentOrdersRequest, RecentOrdersResponse[]>
{
    public override void Configure()
    {
        Get("recent");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("03. Recent")
            .WithDescription("See your most recent Orders")
        );
    }

    public override async Task HandleAsync(RecentOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            BuyerId: User.GetAccountId(),
            Sorting: new(OrderSortingType.OrderDate, SortingDirection.Descending),
            Limit: req.Limit
        );
        Result<GetAllOrdersDto> orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        RecentOrdersResponse[] response = [.. orders.Items.Select(o => o.ToRecentOrdersResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
