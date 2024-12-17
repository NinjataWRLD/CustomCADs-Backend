using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Get.All;

public sealed class GetOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetOrdersRequest, Result<GetOrdersResponse>>
{
    public override void Configure()
    {
        Get("");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("06. All")
            .WithDescription("See all your Orders with Filter, Search, Sorting and Pagination options")
        );
    }

    public override async Task HandleAsync(GetOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            Delivery: req.Delivery,
            OrderStatus: req.OrderStatus,
            BuyerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        Result<GetAllOrdersDto> result = await sender.SendQueryAsync(query, ct);

        Result<GetOrdersResponse> response = new(
            Count: result.Count,
            Items: result.Items.Select(o => o.ToGetOrdersDto()).ToArray()
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
