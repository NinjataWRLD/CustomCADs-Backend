using CustomCADs.Orders.Application.CustomOrders.Queries.GetAll;
using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Orders.Endpoints.CustomOrders.Recent;

public class RecentCustomOrdersEndpoint(IRequestSender sender)
    : Endpoint<RecentCustomOrdersRequest, ICollection<RecentCustomOrdersResponse>>
{
    public override void Configure()
    {
        Get("recent");
        Group<CustomOrdersGroup>();
        Description(d => d.WithSummary("2. I want to see my recent Orders"));
    }

    public override async Task HandleAsync(RecentCustomOrdersRequest req, CancellationToken ct)
    {
        GetAllCustomOrdersQuery query = new(
            BuyerId: User.GetAccountId(),
            Sorting: new(CustomOrderSortingType.OrderDate, SortingDirection.Descending),
            Limit: req.Limit
        );
        GetAllCustomOrdersDto orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ICollection<RecentCustomOrdersResponse> response =
        [
            .. orders.CustomOrders.Select(o => o.ToRecentCustomOrdersResponse())
        ];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
