using CustomCADs.Orders.Application.CustomOrders.Queries.GetAll;

namespace CustomCADs.Orders.Endpoints.CustomOrders.GetAll;

public class GetCustomOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetCustomOrdersRequest, GetCustomOrdersResponse>
{
    public override void Configure()
    {
        Get("");
        Group<CustomOrdersGroup>();
        Description(d => d.WithSummary("7. I want to see all my Orders"));
    }

    public override async Task HandleAsync(GetCustomOrdersRequest req, CancellationToken ct)
    {
        GetAllCustomOrdersQuery query = new(
            DeliveryType: req.DeliveryType,
            OrderStatus: req.OrderStatus,
            BuyerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        GetAllCustomOrdersDto result = await sender.SendQueryAsync(query, ct: ct);

        GetCustomOrdersResponse response = new(
            result.Count,
            result.CustomOrders.Select(o => o.ToGetCustomOrdersDto()).ToArray()
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
