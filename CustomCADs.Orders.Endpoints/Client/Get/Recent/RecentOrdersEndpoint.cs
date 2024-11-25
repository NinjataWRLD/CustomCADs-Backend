using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Endpoints.Client.Get.Recent;

public class RecentOrdersEndpoint(IRequestSender sender)
    : Endpoint<RecentOrdersRequest, ICollection<RecentOrdersResponse>>
{
    public override void Configure()
    {
        Get("recent");
        Group<ClientGroup>();
        Description(d => d.WithSummary("3. I want to see my recent Orders"));
    }

    public override async Task HandleAsync(RecentOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            BuyerId: User.GetAccountId(),
            Sorting: new(OrderSortingType.OrderDate, SortingDirection.Descending),
            Limit: req.Limit
        );
        GetAllOrdersDto orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ICollection<RecentOrdersResponse> response =
        [
            .. orders.Orders.Select(o => o.ToRecentOrdersResponse())
        ];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
