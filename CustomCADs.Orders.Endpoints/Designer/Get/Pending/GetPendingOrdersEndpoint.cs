using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Endpoints.Client;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Pending;

public class GetPendingOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetPendingOrdersRequest, GetPendingOrdersResponse>
{
    public override void Configure()
    {
        Get("pending");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to see all Pending Orders."));
    }

    public override async Task HandleAsync(GetPendingOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            OrderStatus: OrderStatus.Pending,
            DeliveryType: req.DeliveryType,
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetPendingOrdersResponse response = new(
            Count: orders.Count,
            Orders: [.. orders.Orders.Select(o => o.ToGetPendingOrdersDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
