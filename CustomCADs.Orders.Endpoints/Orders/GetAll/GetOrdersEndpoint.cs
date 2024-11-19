using CustomCADs.Orders.Application.Orders.Queries.GetAll;

namespace CustomCADs.Orders.Endpoints.Orders.GetAll;

public class GetOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetOrdersRequest, GetOrdersResponse>
{
    public override void Configure()
    {
        Get("");
        Group<OrdersGroup>();
        Description(d => d.WithSummary("7. I want to see all my Orders"));
    }

    public override async Task HandleAsync(GetOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            DeliveryType: req.DeliveryType,
            OrderStatus: req.OrderStatus,
            BuyerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        GetAllOrdersDto result = await sender.SendQueryAsync(query, ct: ct);

        GetOrdersResponse response = new(
            result.Count,
            result.Orders.Select(o => o.ToGetOrdersDto()).ToArray()
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
