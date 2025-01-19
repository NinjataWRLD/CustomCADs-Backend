using CustomCADs.Orders.Application.CompletedOrders.Queries.GetAll;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Client.Get.All;

public sealed class GetCompletedOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetCompletedOrdersRequest, Result<GetCompletedOrdersResponse>>
{
    public override void Configure()
    {
        Get("");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("07. All")
            .WithDescription("See all your Completed Orders with Filter, Search, Sorting and Pagination options")
        );
    }

    public override async Task HandleAsync(GetCompletedOrdersRequest req, CancellationToken ct)
    {
        GetAllCompletedOrdersQuery query = new(
            Delivery: req.Delivery,
            BuyerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        Result<GetAllCompletedOrdersDto> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetCompletedOrdersResponse> response = new(
            Count: result.Count,
            Items: [.. result.Items.Select(o => o.ToGetCompletedOrdersResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
