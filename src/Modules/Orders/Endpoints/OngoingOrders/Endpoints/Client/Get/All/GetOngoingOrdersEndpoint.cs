using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetAll;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.All;

public sealed class GetOngoingOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetOngoingOrdersRequest, Result<GetOngoingOrdersResponse>>
{
    public override void Configure()
    {
        Get("");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("07. All")
            .WithDescription("See all your Orders with Filter, Search, Sorting and Pagination options")
        );
    }

    public override async Task HandleAsync(GetOngoingOrdersRequest req, CancellationToken ct)
    {
        GetAllOngoingOrdersQuery query = new(
            Delivery: req.Delivery,
            OrderStatus: req.OrderStatus,
            BuyerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        Result<GetAllOngoingOrdersDto> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetOngoingOrdersResponse> response = new(
            Count: result.Count,
            Items: result.Items.Select(o => o.ToGetResponse()).ToArray()
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
